namespace FootballMinimalAPI.Modules;

public class LeaguesModule : CarterModule
{
    public LeaguesModule()
        : base("/Leagues")
    {
        this.RequireAuthorization()
            .WithTags("Leagues");
    }
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/GetAll",
            async ([FromServices] ILeagueService _service, [FromQuery] int page = 1, int size = 10) =>
            {
                var response = await _service.GetAll(page, size);
                return response.data is null ? Results.BadRequest(response) : Results.Ok(response);
            });

        app.MapGet("/Get", async ([FromServices] ILeagueService _service, [FromQuery] int id) =>
        {
            var response = await _service.Get(id);
            return response.data is null ? Results.BadRequest(response) : Results.Ok(response);
        });

        app.MapPost("/Create", async ([FromServices] ILeagueService _service, [FromBody] CreateLeagueDTO request) =>
        {
            var response = await _service.Create(request);
            return response.data is false ? Results.BadRequest(response) : Results.Created();
        });

        app.MapPut("/Update", async ([FromServices] ILeagueService _service, [FromBody] UpdateLeagueDTO request) =>
        {
            var response = await _service.Update(request);
            return response.data is false ? Results.BadRequest(response) : Results.Ok(response);
        });
        app.MapDelete("/Delete", async ([FromServices] ILeagueService _service, [FromQuery] int id) =>
        {
            var response = await _service.Delete(id);
            return response.data is false ? Results.BadRequest(response) : Results.Ok(response);
        });
    }
}
