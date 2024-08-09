

namespace FootballMinimalAPI.Modules;

public class AuthModule : CarterModule
{

    public AuthModule()
        : base("/Auth")
    {
        this.WithTags("Auth");
    }
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/login", async ([FromBody] LoginDTO request, [FromServices] IAuthService _service) =>
        {
            var response = await _service.Login(request);
            return response.data is null ? Results.BadRequest(response) :  Results.Ok(response);
        });

        app.MapPost("/Register", async ([FromBody] RegisterDTO request, [FromServices] IAuthService _service) =>
        {
            var response = await _service.Register(request);
            return response.data is false ? Results.BadRequest(response): Results.Created();
        });
        app.MapPost("/Refresh", async ([FromBody] RefreshDTO request, [FromServices] IAuthService _service) =>
        {
            var response = await _service.Refresh(request);
            return response.data is null ? Results.BadRequest(response) : Results.Ok(response);
        });

      
    }


}