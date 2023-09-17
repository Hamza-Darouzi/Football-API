



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("appsettings.json").Build();
builder.Services.AddDbContext<AppDbContext>(
                              options => options.UseSqlServer(
                                  builder.Configuration.GetConnectionString("DefaultConnection")
                                  , x => x.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
                                  )
                              ) ;

builder.Services.AddTransient<IUnitOfWork,UnitOfWork>();
builder.Services.AddTransient(typeof(IBaseService<>), typeof(BaseRepository<>));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
