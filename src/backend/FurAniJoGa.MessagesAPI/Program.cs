using System.Text.Json;
using FurAniJoGa.Domain;
using FurAniJoGa.Infrastructure;
using FurAniJoGa.Infrastructure.Managers;
using MassTransit;
using MessagesAPI.MessageQueue.Consumers;
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
    x.UseNpgsql("User Id=postgres;Password=postgres;Host=localhost;Port=5432;Database=postgres");
});
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<MessagePublishedConsumer>();
    config.UsingInMemory((context, configurator) =>
    {
        configurator.Host();
        configurator.ReceiveEndpoint(new TemporaryEndpointDefinition(), x =>
        {
            x.ConfigureConsumer<MessagePublishedConsumer>(context);
        });
        configurator.ConfigureEndpoints(context);
    });
});
builder.Services.AddScoped<MessagePublishedConsumer>();
builder.Services.AddScoped<IMessageFactory, SampleMessageFactory>();
builder.Services.AddMassTransitHostedService();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x =>
{
    x.AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();