
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddPresentation();


var app = builder.Build();
app.MapControllers();
app.UseCors("Default");
app.UseSwagger();
app.UseSwaggerUI(o =>
{
    o.DocExpansion(DocExpansion.None);
});
app.UseHttpsRedirection();
app.Run();

