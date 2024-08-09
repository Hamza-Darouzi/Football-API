
namespace Football.Domain.Tools.Pagination;

public static class Pagination 
{
    public static PaginationResponse<T> Paginate<T>(this ICollection<T> source, int page = 1 , int size=10)
    {
        page = (page < 1) ? 1:page;
        size = (size < 1) ? 10: size;
        var count = source.Count();
        var pages = (int)Math.Ceiling((decimal)count / size);
        var result = source.Skip((page - 1) * size).Take(size);
        return new PaginationResponse<T>(Values: result, pages: pages);
    }
}
