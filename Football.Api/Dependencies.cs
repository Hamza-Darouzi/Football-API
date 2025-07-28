

namespace Football.Api;

public static class Dependencies
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddRateLimiter(limiterOptions =>
        {
            limiterOptions.AddPolicy("auth", context =>
                RateLimitPartition.GetFixedWindowLimiter(
                    context.Connection.RemoteIpAddress.ToString(),
                    partition => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = 5,
                        Window = TimeSpan.FromMinutes(1)
                    }));
        });
        services.AddRateLimiter(limiterOptions =>
        {
            limiterOptions.AddPolicy("fixed", context =>
                RateLimitPartition.GetFixedWindowLimiter(
                    context.Connection.RemoteIpAddress.ToString(),
                    partition => new FixedWindowRateLimiterOptions
                    {
                        AutoReplenishment = true,
                        PermitLimit = 50,
                        Window = TimeSpan.FromMinutes(1)
                    }));
        });
        services.AddHttpContextAccessor();
        services.AddEndpointsApiExplorer();
        // Customeize CORS According To Your Requirements
        services.AddCors(o =>
        {
            o.AddPolicy("Default", policy =>
            {
                policy.AllowAnyOrigin();
                policy.AllowAnyMethod();
                policy.AllowAnyHeader();
                policy.SetPreflightMaxAge(TimeSpan.FromMinutes(10));
            });
        });
        services.AddSwaggerGen();
        services.AddSwaggerGen(options =>
        {
            // JWT Bearer Token Definition
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please insert JWT with Bearer into field (e.g., 'Bearer {token}')",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });
            // Global Security Requirements for both JWT and API Key
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "Bearer",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                     },
                    Array.Empty<string>()
                } 
            });

            // Swagger Document Information
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Football API",
                Contact = new OpenApiContact
                {
                    Name = "Eng Hamza Darouzi",
                    Email = "darouzihamza@gmail.com",
                }
            });
        });

        return services;
    }

}
