

namespace Football.Infrastructure.Repository.Users;

public interface IUserRepository<T> : IBaseRepository<T> where T : User
{
    Task<IEnumerable<T>> GetAllByRole(string role);
    Task<IdentityResult> AddWithRole(T user, string role, string password);
    Task<IdentityResult> AddWithRole(T user, string role );
    Task<IdentityResult> AddWithRole(T user, ICollection<string> roles);
    Task<IdentityResult> TryModifyPassword(T user, string? newPassword);
}
