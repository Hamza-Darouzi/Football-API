
namespace Football.Infrastructure.Services.Files;

public class FileService(ILogger<FileService> logger) : IFileService
{
    private const int MaxFileSize = 5 * 1024 * 1024; // 5MB
    private static readonly string[] ValidImageExtensions = { "jpg", "jpeg", "png", "svg", "webp", "avif" };
    private static readonly string[] ValidPdfExtensions = { "pdf" };
    private readonly ILogger<FileService> _logger = logger;

    public bool IsValidImage(IFormFile file) =>
        IsFileValid(file, ValidImageExtensions);

    public bool IsValidPDF(IFormFile file) =>
        IsFileValid(file, ValidPdfExtensions);

    private static bool IsFileValid(IFormFile file, string[] validExtensions)
    {
        if (file == null || file.Length == 0 || file.Length > MaxFileSize)
            return false;

        var fileExt = Path.GetExtension(file.FileName).TrimStart('.').ToLower();
        return validExtensions.Contains(fileExt);
    }

    public async Task<Result> UploadImageAsync(IFormFile file, string folderName)
    {
        try
        {
            var filePath = GetFilePath(folderName, file.FileName);

            if (File.Exists(filePath))
                return new Result(filePath, Error.FileExist);

            if (!IsValidImage(file))
                return new Result(null, Error.InvalidFile);

            await SaveFileAsync(file, filePath);
            return new Result(filePath, Error.None);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error uploading image");
            return new Result(null, Error.InvalidFile);
        }
    }

    public async Task<Result> UploadImageAsyncV2(IFormFile file, string folderName)
    {
        var folderPath = EnsureFolder(folderName);
        var newFileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}.webp";
        var filePath = Path.Combine(folderPath, newFileName).Replace("\\", "/");

        if (File.Exists(filePath))
            return new Result(filePath, Error.FileExist);

        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        if (!IsValidImage(memoryStream))
            return new Result(null, Error.InvalidFile);

        try
        {
            memoryStream.Position = 0;
            using var image = await Image.LoadAsync(memoryStream);
            var encoder = new WebpEncoder { Quality = 80 };

            await using var outputStream = File.Create(filePath);
            await image.SaveAsync(outputStream, encoder);

            return new Result(filePath, Error.None);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Image conversion to WebP failed");
            return new Result(null, Error.ConversionFailed);
        }
    }

    public async Task<Result> UploadImageAsyncV3(IFormFile file, string folderName)
    {
        var folderPath = EnsureFolder(folderName);
        var avifFileName = $"{Path.GetFileNameWithoutExtension(file.FileName)}.avif";
        var finalFilePath = Path.Combine(folderPath, avifFileName).Replace("\\", "/");

        if (File.Exists(finalFilePath))
            return new Result(finalFilePath, Error.FileExist);

        // Validate image before processing
        using (var validationStream = new MemoryStream())
        {
            await file.CopyToAsync(validationStream);
            validationStream.Position = 0;

            if (!IsValidImage(validationStream))
                return new Result(false, Error.InvalidFile);
        }

        var tempInputPath = Path.Combine(Path.GetTempPath(), file.FileName);
        var tempOutputPath = Path.Combine(Path.GetTempPath(), avifFileName);

        try
        {
            // Save original to temp location
            await using (var tempStream = File.Create(tempInputPath))
            {
                await file.CopyToAsync(tempStream);
            }

            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "C:\\ffmpeg\\bin\\ffmpeg.exe",
                    Arguments = $"-y -i \"{tempInputPath}\" -f avif -still-picture 1 -c:v libaom-av1 -crf 30 \"{tempOutputPath}\"",
                    RedirectStandardError = true,
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();
            await process.WaitForExitAsync();

            if (process.ExitCode != 0 || !File.Exists(tempOutputPath))
            {
                var error = await process.StandardError.ReadToEndAsync();
                _logger.LogError("AVIF conversion failed: {Error}", error);
                return new Result(false, Error.ConversionFailed);
            }

            File.Move(tempOutputPath, finalFilePath);
            return new Result(finalFilePath, Error.None);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "AVIF conversion error");
            return new Result(false, Error.ConversionFailed);
        }
        finally
        {
            SafeDeleteFile(tempInputPath);
            SafeDeleteFile(tempOutputPath);
        }
    }

    public async Task<Result> UploadPDF(IFormFile file, string folderName)
    {
        try
        {
            var filePath = GetFilePath(folderName, file.FileName);

            // Special handling: return success if file exists
            if (File.Exists(filePath))
                return new Result(filePath, Error.None);

            if (!IsValidPDF(file))
                return new Result(null, Error.InvalidFile);

            await SaveFileAsync(file, filePath);
            return new Result(filePath, Error.None);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "PDF upload failed");
            return new Result(null, Error.InvalidFile);
        }
    }

    private bool IsValidImage(Stream stream)
    {
        try
        {
            if (stream.CanSeek) stream.Position = 0;
            var format = Image.DetectFormat(stream);
            if (format == null) return false;

            if (stream.CanSeek) stream.Position = 0;
            using var image = Image.Load(stream);
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Invalid image stream");
            return false;
        }
    }

    private static string EnsureFolder(string folderName)
    {
        var folderPath = Path.Combine("wwwroot", folderName);
        Directory.CreateDirectory(folderPath);
        return folderPath;
    }

    private static string GetFilePath(string folderName, string fileName)
    {
        var folderPath = EnsureFolder(folderName);
        return Path.Combine(folderPath, fileName).Replace("\\", "/");
    }

    private static async Task SaveFileAsync(IFormFile file, string filePath)
    {
        await using var stream = File.Create(filePath);
        await file.CopyToAsync(stream);
    }

    private void SafeDeleteFile(string path)
    {
        try
        {
            if (File.Exists(path)) File.Delete(path);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Could not delete temp file: {Path}", path);
        }
    }
}