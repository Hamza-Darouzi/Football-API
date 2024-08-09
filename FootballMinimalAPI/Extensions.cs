using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace FootballMinimalAPI;

public static class Extensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IClubService, ClubService>();
        services.AddTransient<IPlayersService, PlayersService>();
        services.AddTransient<ILeagueService, LeagueService>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        return services;
    }
    public static IServiceCollection ConfigureSecurity(this IServiceCollection services, IConfiguration configuration)
    {

        services.Configure<JwtOptions>(options => configuration.GetSection(JwtOptions.Jwt).Bind(options));
        var jwtOptions = configuration.GetSection(JwtOptions.Jwt).Get<JwtOptions>()!;
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        RequireExpirationTime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key))
                    };
                })
        .Services
        .AddAuthorization(authorizationOptions =>
        {
            authorizationOptions.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
            .RequireAuthenticatedUser()
        .Build();
        })
        .AddTransient<IJwtBearerGenerator, JwtBearerGenerator>();
        return services;
    }

    public static IServiceCollection ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {

        services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(options => options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
        services.AddEndpointsApiExplorer();
        services.AddCarter();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v2", new OpenApiInfo
            {
                Version = "v2",
                Title = "Football API",
                Description = "Simple CRUD Project",
                Contact = new OpenApiContact
                {
                    Name = "Eng Hamza Darouzi",
                    Email = "darouzihamza@gmail.com",
                }
            });
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT with Bearer into field",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });
        });

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddDbContext<AppDbContext>(
                                      options => options.UseSqlServer(
                                          configuration.GetConnectionString("Default")
                                          , x => x.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
                                          )
                                      );

        services.AddCors(o => o.AddPolicy("AllowAll", o =>
        {
            o.AllowAnyHeader();
            o.AllowAnyMethod();
            o.AllowAnyOrigin();
        }));

        return services;

    }

    public static WebApplication UseMiddleWares(this WebApplication app)
    {

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(o => {
                o.DocExpansion(DocExpansion.None);
                o.SwaggerEndpoint("/swagger/v2/swagger.json", "Football API V2");
            });
        }

        app.UseCors("AllowAll");
        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapCarter();

        return app;

    }
}
