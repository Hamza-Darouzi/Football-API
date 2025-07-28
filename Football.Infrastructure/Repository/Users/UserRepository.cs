
namespace Football.Infrastructure.Repository.Users;

public class UserRepository<T> : BaseRepository<T>, IUserRepository<T> where T : User
{
    private UserManager<T> _userManager;

    public UserRepository(UserManager<T> userManager, AppDbContext context)
        : base(context)
    {
        _userManager = userManager;
    }

    public async Task<IEnumerable<T>> GetAllByRole(string role)
    {
        return await _userManager.GetUsersInRoleAsync(role);
    }

    public async Task<IdentityResult> AddWithRole(T user, string role, string password)
    {
        IdentityResult identityResult = await _userManager.CreateAsync(user, password);

        if (!identityResult.Succeeded)
            return identityResult;

        identityResult = await _userManager.AddToRoleAsync(user, role);
        return identityResult;
    }
    public async Task<IdentityResult> AddWithRole(T user, string role)
    {
        IdentityResult identityResult = await _userManager.CreateAsync(user);

        if (!identityResult.Succeeded)
            return identityResult;

        identityResult = await _userManager.AddToRoleAsync(user, role);
        return identityResult;
    }

    public async Task<IdentityResult> AddWithRole(T user, ICollection<string> roles)
    {
        IdentityResult identityResult = await _userManager.CreateAsync(user);

        if (!identityResult.Succeeded)
            return identityResult;

        identityResult = await _userManager.AddToRolesAsync(user, roles);
        return identityResult;
    }

    public async Task<IdentityResult> TryModifyPassword(T user, string? newPassword)
    {
        if (newPassword is null)
            return IdentityResult.Failed();

        string resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

        IdentityResult TrychangePass = await _userManager.ResetPasswordAsync(
            user,
            resetToken,
            newPassword!
        );

        return TrychangePass;
    }
}
