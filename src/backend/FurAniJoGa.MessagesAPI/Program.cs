using System.Text.Json;
using FurAniJoGa.Domain;
using FurAniJoGa.Infrastructure;
using FurAniJoGa.Infrastructure.Managers;
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
    x.UseNpgsql("User Id=postgres;Password=postgres;Host=database;Port=5432;Database=postgres");
});
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddSignalR();

builder.Services.AddMassTransit(configurator =>
{
    var host = builder.Configuration["RABBITMQ_HOST"];
    if (host is null)
    {
        throw new ArgumentNullException(nameof(host), "Host for RabbitMq is not provided");
    }

    var exchange = builder.Configuration["RABBITMQ_EXCHANGE"];
    if (exchange == null)
    {
        throw new ArgumentNullException(nameof(exchange), "RabbitMq Exchange name is not provided");
    }
    
    configurator.UsingRabbitMq((registrationContext, factory) =>
    {
        factory.Host(host, "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });
        factory.ReceiveEndpoint(e =>
        {
            e.Bind(exchange);
        });
        factory.ConfigureEndpoints(registrationContext);
    });
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