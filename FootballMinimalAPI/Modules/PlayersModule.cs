namespace FootballMinimalAPI.Modules;

public class PlayersModule : CarterModule
{
    public PlayersModule()
        : base("/Players")
    {
        this.RequireAuthorization()
            .WithTags("Players");
    }
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/GetAll",
            async ([FromServices] IPlayersService _service, [FromQuery] int page = 1, int size = 10) =>
            {
                var response = await _service.GetAll(page, size);
                return response.data is null ? Results.BadRequest(response) : Results.Ok(response);
            });

        app.MapGet("/Get", async ([FromServices] IPlayersService _service, [FromQuery] int id) =>
            {
                var response = await _service.Get(id);
                return response.data is null ? Results.BadRequest(response) : Results.Ok(response);
            });

        app.MapPost("/Create", async ([FromServices] IPlayersService _service, [FromBody] CreatePlayerDTO request) =>
            {
                var response = await _service.Create(request);
                return response.data is false ? Results.BadRequest(response) : Results.Created();
            });

        app.MapPut("/Update", async ([FromServices] IPlayersService _service, [FromBody] UpdatePlayerDTO request) =>
            {
                var response = await _service.Update(request);
                return response.data is false ? Results.BadRequest(response) : Results.Ok(response);
            });
        app.MapDelete("/Delete", async ([FromServices] IPlayersService _service, [FromQuery] int id) =>
            {
                var response = await _service.Delete(id);
                return response.data is false ? Results.BadRequest(response) : Results.Ok(response);
            });
    }
}
