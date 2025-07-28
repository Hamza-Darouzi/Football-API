

namespace Football.Application.Features.Auth.Commands.Login;

public class LoginRequestHandler(UserManager<User> manager,
                                 TokenServices tokenServices,
                                 IHttpContextAccessor httpContext,
                                 IUnitOfWork unitOfWork) : IRequestHandler<LoginRequest, Result>
{
    private readonly UserManager<User> _manager = manager;
    private readonly TokenServices _tokenService = tokenServices;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContext;

    public async Task<Result> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var lang = Extensions.DetectLanguage(_httpContextAccessor);
          
            var user = await _unitOfWork.Users.GetOneAsync(u => u.Email.Equals(request.email)).FirstOrDefaultAsync(cancellationToken);

            if (user is null)
                return new Result(null, Error.UserNotFound);

            var isPasswordMatch = await _manager.CheckPasswordAsync(user, request.password);

            if (!isPasswordMatch)
                return new Result(null, Error.WrongPassword);

            var isPhoneNumberConfirmed = await _manager.IsPhoneNumberConfirmedAsync(user);
            if (!isPhoneNumberConfirmed)
                return new Result(null, Error.PhoneNumberConfirmation);

            var roles = await _manager.GetRolesAsync(user);
            var response = await _tokenService.GenerateToken(user, roles.ToList(), cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new Result(response, Error.None);

        }
        catch (Exception ex)
        {
            return new Result(null, new Error("500", ex.Message));
        }
    }
   
}