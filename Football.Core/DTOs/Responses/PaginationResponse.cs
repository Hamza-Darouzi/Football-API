
namespace Football.Core.DTOs.Responses;

public record PaginationResponse<T>(IEnumerable<T> Values , int pages);
