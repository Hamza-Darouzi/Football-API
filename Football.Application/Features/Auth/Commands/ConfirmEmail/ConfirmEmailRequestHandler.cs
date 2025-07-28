



namespace Football.Application.Features.Auth.Commands.ConfirmEmail;

public class ConfirmEmailRequestHandler(UserManager<User> manager,
                                 IUserRepository<User> userRepository,
                                 IUnitOfWork unitOfWork) : IRequestHandler<ConfirmEmailRequest, Result>
{
    private readonly UserManager<User> _manager = manager;
    private readonly IUserRepository<User> _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result> Handle(ConfirmEmailRequest request, CancellationToken cancellationToken)
    {
        try
        {

          
            var user = await _userRepository.GetOneAsync(u => u.Email.Equals(request.email.ToLower()))
                                           .FirstOrDefaultAsync(cancellationToken);
            if (user is null)
                return new Result(false, Error.UserNotFound);

            if (user.ResetToken != request.token)
                return new Result(false, Error.InvalidOTP);
            else
            {
                user.EmailConfirmed = true;
                user.ResetToken = null;
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return new Result(true, Error.None);
        }
        catch (Exception ex)
        {
            return new Result(false, new Error("500", ex.Message));
        }
    }
}
