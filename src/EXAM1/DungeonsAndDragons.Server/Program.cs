using DungeonsAndDragons.Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<IFightSimulator, HonestFightSimulator>();

var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
