

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureInfrastructure(builder.Configuration)
                .ConfigureSecurity(builder.Configuration)
                .ConfigureServices();

var app = builder.Build();
app.UseMiddleWares();
app.Run();
