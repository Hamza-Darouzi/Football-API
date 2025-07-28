


namespace Football.Application.Features.Auth.Commands.ResetPassword;

public class ResetPasswordRequestHandler(UserManager<User> manager,
                                 IUserRepository<User> userRepository,
                                 IUnitOfWork unitOfWork) : IRequestHandler<ResetPasswordRequest, Result>
{
    private readonly UserManager<User> _manager = manager;
    private readonly IUserRepository<User> _userRepository = userRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetOneAsync(u => u.Email.Equals(request.email.ToLower()))
                                            .FirstOrDefaultAsync(cancellationToken);

            if (user is null)
                return new Result(null, Error.UserNotFound);
            if(request.token == user.ResetToken)
            {
                await _manager.RemovePasswordAsync(user);
                await _manager.AddPasswordAsync(user,request.newPassword);
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
