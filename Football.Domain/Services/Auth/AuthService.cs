

namespace Football.Domain.Services.Auth;

public class AuthService(IJwtBearerGenerator jwtBearerGenerator,IUnitOfWork unitOfWork) : IAuthService
{
    private readonly IJwtBearerGenerator _jwtBearerGenerator = jwtBearerGenerator;
    private readonly IUnitOfWork _unit = unitOfWork;
   
    public async Task<Result> Login(LoginDTO request)
    {
        try
        {
            var user = await _unit.Users.FindAsync(u => u.Username.Equals(request.userName));

            if (user is null)
                return new Result(null, "Invalid Username");

            if (!BCrypt.Net.BCrypt.Verify(request.passWord, user.PasswordHash))
                return new Result(null, "Wrong Password");

            user.RefreshToken = new RefreshToken
            {
                Token = _jwtBearerGenerator.GenerateUniqueRefreshToken(),
                ExpireAt = DateTime.Now.AddDays(7),
            };

            var response = new LoginResponse(user.Id, _jwtBearerGenerator.Generate(new()),user.RefreshToken.Token);
            await _unit.Users.Update(user);
            return new Result(response, "Done");
        }
        catch (Exception ex)
        {
            return new Result(null, ex.Message);
        }

    }
    public async Task<Result> Register(RegisterDTO request)
    {
        try
        {
            if (request is null)
                return new Result(false, "Invalid Data");

            if(await _unit.Users.Exist(u=>u.Username.Equals(request.username)))
                return new Result(false, "Username Already Exist");

            var user = new User
            {
                Username = request.username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.password),
                RefreshToken = new() { 
                                                Token = _jwtBearerGenerator.GenerateUniqueRefreshToken() ,
                                                ExpireAt = DateTime.Now.AddDays(7)
                }
            };
            await _unit.Users.Create(user);
           return new Result(true, "Done");
        }
        catch (Exception ex)
        {
            return new Result(false, ex.Message);
        }
    }
    public async Task<Result> Refresh(RefreshDTO request)
    {
        try
        {
            var user = await _unit.Users.FindAsync(u => u.Username.Equals(request.username));
            var days = user.RefreshToken.ExpireAt - DateTime.Now;

            if (user.RefreshToken.Token.Equals(request.refreshToken) && days.Days <= 0)
                return new Result(false, "Token is expired");

            if (!user.RefreshToken.Token.Equals(request.refreshToken))
                return new Result(null,"Invalid Token");

            var response = _jwtBearerGenerator.GenerateAccessTokenFromRefreshToken(request.refreshToken);

            return new Result(response, "Done");
        }
        catch (Exception ex)
        {
            return new Result(null, ex.Message);
        }

    }
}
