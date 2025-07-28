
namespace Football.Application.Features.Auth.Commands.Refresh;

public class RefreshRequestHandler(IUnitOfWork unitOfWork,
                             TokenValidationParameters tokenValidationParameters,
                             UserManager<User> userManager,
                             TokenServices services) : IRequestHandler<RefreshRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly TokenValidationParameters _tokenValidationParameters = tokenValidationParameters;
    private readonly UserManager<User> _userManager = userManager;
    private readonly TokenServices _services = services;


    public async Task<Result> Handle(RefreshRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            // Clone parameters to avoid modifying the shared instance
            var validationParameters = _tokenValidationParameters.Clone();
            validationParameters.ValidateLifetime = false; // Disable lifetime check for expired tokens


            var storedToken = await _unitOfWork.RefreshTokens
                .GetOneAsync(r => r.Token == new Guid(request.refreshToken))
                .FirstOrDefaultAsync(cancellationToken);

            if (storedToken == null)
                return new Result(false, new Error("400", "Invalid RefreshToken"));

            if (storedToken.ExpiryDate < DateTime.UtcNow)
                return new Result(false, new Error("400", "Expired RefreshToken"));

            var user = await _unitOfWork.Users
                .GetOneAsync(u => u.Id == storedToken.UserId)
                .FirstOrDefaultAsync(cancellationToken);

            if (user == null)
                return new Result(false, new Error("400", "User not found"));

            _unitOfWork.RefreshTokens.Remove(storedToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            var roles = await _userManager.GetRolesAsync(user);
            var response= await _services.GenerateToken(user, roles.ToList(), cancellationToken);
             
            return new Result(
                 response,
                Error.None
            );
        }
        catch (SecurityTokenException ex)
        {
            return new Result(false, new Error("400", $"Token validation failed: {ex.Message}"));
        }
        catch (Exception ex)
        {
            return new Result(false, new Error("500", $"Internal server error: {ex.Message}"));
        }
    }
}