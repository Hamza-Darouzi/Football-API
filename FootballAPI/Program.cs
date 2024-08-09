

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Configuration.AddJsonFile("appsettings.json").Build();
builder.Services.AddEndpointsApiExplorer();

builder.Services.ConfigureInfrastructre(builder.Configuration)
                .ConfigureSecurity(builder.Configuration)
                .ConfigureServices();

var app = builder.Build();
app.UseMiddleWares();
app.Use(async (context, next) =>
{
    context.Response.Headers.Add("X-Developed-By", "Eng Hamza Darouzi");
    await next.Invoke();
});
app.Run();
