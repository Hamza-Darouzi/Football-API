namespace Football.Application.Features.Auth.Commands.ForgetPassword;

public class ForgetPasswordRequestHandler(UserManager<User> manager,
                                 IUserRepository<User> userRepository,
                                 IMailService mailService,
                                 IUnitOfWork unitOfWork) : IRequestHandler<ForgetPasswordRequest, Result>
{
    private readonly UserManager<User> _manager = manager;
    private readonly IUserRepository<User> _userRepository = userRepository;
    private readonly IMailService _mailService = mailService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(ForgetPasswordRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetOneAsync(u => u.Email.Equals(request.email))
                                             .FirstOrDefaultAsync(cancellationToken);

            if (user is null)
                return new Result(null, Error.UserNotFound);

            var token =  Extensions.GenerateNumericCodes();
            user.ResetToken = token;
            user.EmailConfirmed = false;
            user.PhoneNumberConfirmed = false;
            await _mailService.SendEmail(new EmailDto(user.Email, "Reset Code! ", $"your reset code : {token} , please don't share this code"));
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new Result(true, Error.None);

        }
        catch (Exception ex)
        {
            return new Result(false, new Error("500", ex.Message));
        }
    }
}

