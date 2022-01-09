using DungeonsAndDragons.Database.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var connectionString = builder.Environment.IsDevelopment()
                           ? builder.Configuration.GetConnectionString("PostgreSQLDebugConnectionString")
                           : builder.Configuration.GetConnectionString("PostgreSQLDevelopmentConnectionString");
builder.Services.AddDbContext<GameDbContext>(opt => opt.UseNpgsql(connectionString));
var app = builder.Build();

app.UseAuthorization();

app.MapControllers();

app.Run();
