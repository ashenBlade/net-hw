using System.Text.Json;
using FurAniJoGa.Domain;
using FurAniJoGa.Infrastructure;
using FurAniJoGa.Infrastructure.Repositories;
using MassTransit;
using MessagesAPI;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(json =>
{
    json.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<MessagesDbContext>(x =>
{
    var database = builder.Configuration["DB_DATABASE"] ?? throw new ArgumentNullException(null, "Database name is not provided");
    var host = builder.Configuration["DB_HOST"] ?? throw new ArgumentNullException(null, "Database host is not provided");;
    var username = builder.Configuration["DB_USERNAME"] ?? throw new ArgumentNullException(null, "Database username is not provided");;
    var password = builder.Configuration["DB_PASSWORD"]  ?? throw new ArgumentNullException(null, "Database password is not provided");;
    var portString = builder.Configuration["DB_PORT"] ?? throw new ArgumentNullException(null, "Database port is not provided");;
    if (!int.TryParse(portString, out var port))
    {
        throw new ArgumentException($"Database port must be integer. Given: {portString}");
    }

    x.UseNpgsql($"User Id={username};Password={password};Host={host};Database={database};Port={port}");
});
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddSignalR();

builder.Services.AddMassTransit(configurator =>
{
    throw new NotImplementedException("Implement connection to MassTransit pls");
});
builder.Services.AddScoped<IMessageFactory, SampleMessageFactory>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x =>
{
    x.WithOrigins("http://localhost:8080");
    x.AllowAnyHeader();
    x.AllowAnyMethod();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/chat");

app.Run();