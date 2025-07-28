

namespace Football.Infrastructure;

public static class Dependencies
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<UserManager<User>>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped(typeof(IUserRepository<>), typeof(UserRepository<>));
        services.AddTransient<IFileService, FileService>();
        services.AddTransient<IMailService, MailService>();
        services.AddTransient<IJwtProvider, JwtProvider>();
        services.AddTransient<TokenServices>();

        services.AddDbContext<AppDbContext>(o =>
        {
            o.UseLazyLoadingProxies();
            o.UseNpgsql(configuration.GetConnectionString("Postgre"));
        })
        .AddIdentity<User, IdentityRole<int>>(options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ -أاإآلىئبتثجحخعغرزقفصضسشدذرزؤطظهكمنوية";
        })
         .AddEntityFrameworkStores<AppDbContext>()
         .AddDefaultTokenProviders();

        services.Configure<DataProtectionTokenProviderOptions>(op =>
          op.TokenLifespan = TimeSpan.FromHours(1));

        return services;
    }
}
