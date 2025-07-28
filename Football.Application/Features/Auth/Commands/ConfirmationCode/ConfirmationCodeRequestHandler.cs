



namespace Football.Application.Features.Auth.Commands.ConfirmationCode;

public class ConfirmationCodeRequestHandler(UserManager<User> manager,
                                 IUserRepository<User> userRepository,
                                 IMailService mailService,
                                 IUnitOfWork unitOfWork) : IRequestHandler<ConfirmationCodeRequest, Result>
{
    private readonly UserManager<User> _manager = manager;
    private readonly IUserRepository<User> _userRepository = userRepository;
    private readonly IMailService _mailService = mailService;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(ConfirmationCodeRequest request, CancellationToken cancellationToken)
    {
        try
        {
          
            var user = await _userRepository.GetOneAsync(u => u.Email.Equals(request.email))
                                             .FirstOrDefaultAsync(cancellationToken);

            if (user is null)
                return new Result(null, Error.UserNotFound);

            var token = Extensions.GenerateNumericCodes();
            user.ResetToken = token;
            if(user.Email is not null)
                await _mailService.SendEmail(new EmailDto(user.Email, "تفعيل الحساب", $"{token} رمز التفعيل الخاص بك هو "));
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new Result(true, Error.None);

        }
        catch (Exception ex)
        {
            return new Result(false, new Error("500", ex.Message));
        }
    }
}