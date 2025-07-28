
namespace Football.Application;

public static class Extensions
{
    public static async Task<PaginationResponseDTO<T>> PaginateAsync<T>(
    this IEnumerable<T> source,
    int page = 1,
    int size = 10
)
    where T : class
    {
        if (page <= 0)
        {
            page = 1;
        }

        if (size <= 0)
        {
            size = 10;
        }
        var total = await CountAsync(source);
        var pages = (int)Math.Ceiling((decimal)total / size);
        var result = source.Skip((page - 1) * size).Take(size).ToList();

        return new PaginationResponseDTO<T>(Values: result, Pages: pages);
    }

    private static async Task<int> CountAsync<T>(IEnumerable<T> source)
    {
        var count = 0;
        foreach (var item in source)
        {
            count++;
            await Task.Yield();
        }
        return count;
    }
    public static async Task ForEachAsync<T>(this List<T> list, Func<T, Task> func)
    {
        foreach (var value in list)
        {
            await func(value);
        }
    }
    public static void CleanFiles(this string folderName)
    {
        if (Directory.Exists(folderName))
        {
            var files = Directory.GetFiles(folderName);
            var directories = Directory.GetDirectories(folderName);
            foreach (var directory in directories)
            {
                Directory.Delete(directory, true);
            }
            foreach (var file in files)
            {
                File.Delete(file);
            }
            Directory.Delete(folderName);
        }
    }
    public static Languages DetectLanguage(IHttpContextAccessor httpContextAccessor)
    {
        var languageHeader = httpContextAccessor.HttpContext?.Request.Headers["Accept-Language"].ToString()!;
        if (languageHeader is null)
            return Languages.ar;
        Enum.TryParse(languageHeader, true, out Languages lang);
        return lang;
    }

    public static string NormalizePhoneNumber(string phoneNumber)
    {
        // Normalize the phone number
        if (phoneNumber.StartsWith("+963"))
            phoneNumber = phoneNumber[4..]; // Remove the "+963" prefix

        else if (phoneNumber.StartsWith("00963"))
            phoneNumber = phoneNumber[2..]; // Remove the "00963" prefix

        else if (phoneNumber.StartsWith("09"))
            phoneNumber = "963" + phoneNumber[1..]; // Remove the leading "0" and prepend "963"

        // Check if the number starts with "963"
        if (!phoneNumber.StartsWith("963"))
            phoneNumber = "963" + phoneNumber;

        if (phoneNumber.Length != 12)
            return null;    // Invalid phone number

        return phoneNumber;
    }
    
    public static string GenerateNumericCodes(int codeLength = 6, int numberOfCodes = 1)
    {
        var _random = new Random();
        var code = new StringBuilder();
        for (int j = 0; j < codeLength; j++)
            code.Append(_random.Next(9));

        return code.ToString();
    }
}
