
namespace Football.Infrastructure.Services.Files;

public interface IFileService
{
    bool IsValidImage(IFormFile file);
    bool IsValidPDF(IFormFile file);
    Task<Result> UploadPDF(IFormFile file, string folderName);
    Task<Result> UploadImageAsync(IFormFile file, string folderName);
    Task<Result> UploadImageAsyncV2(IFormFile file, string folderName);
    Task<Result> UploadImageAsyncV3(IFormFile file, string folderName);
}
