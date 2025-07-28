
namespace Football.Domain.Common;

public record PaginationResponseDTO<T>(IEnumerable<T>? Values, int? Pages = 0);
