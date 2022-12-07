using System.Text.Json;
using FurAniJoGa.Domain;
using FurAniJoGa.Infrastructure;
using FurAniJoGa.Infrastructure.Repositories;
using FurAniJoGa.RabbitMq.Contracts.Events;
using MassTransit;
using MessagesAPI.Consumers;
using MessagesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MessagesAPI;

public static class ServicesExtensions
{
    public static void AddServicesForMessagesApi(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddControllers().AddJsonOptions(json =>
        {
            json.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddSingleton(new Forum());
        services.AddDbContext<MessagesDbContext>(x =>
        {
            var database = configuration["DB_DATABASE"] ?? throw new ArgumentNullException(null, "Database name is not provided");
            var host = configuration["DB_HOST"] ?? throw new ArgumentNullException(null, "Database host is not provided");;
            var username = configuration["DB_USERNAME"] ?? throw new ArgumentNullException(null, "Database username is not provided");;
            var password = configuration["DB_PASSWORD"]  ?? throw new ArgumentNullException(null, "Database password is not provided");;
            var portString = configuration["DB_PORT"] ?? throw new ArgumentNullException(null, "Database port is not provided");;
            if (!int.TryParse(portString, out var port))
            {
                throw new ArgumentException($"Database port must be integer. Given: {portString}");
            }

            x.UseNpgsql($"User Id={username};Password={password};Host={host};Database={database};Port={port}");
        });
        services.AddScoped<IMessageRepository, MessageRepository>();
        services.AddScoped<IRequestRepository, RequestsRepository>();
        services.AddSignalR();


        services.AddMassTransit(configurator =>
        {
            var host = configuration["RABBITMQ_HOST"];
            if (host is null)
            {
                throw new ArgumentNullException(nameof(host), "Host for RabbitMq is not provided");
            }

            var exchange = configuration["RABBITMQ_EXCHANGE"];
            if (exchange == null)
            {
                throw new ArgumentNullException(nameof(exchange), "RabbitMq Exchange name is not provided");
            }
            
            configurator.AddConsumer<FileAndMetadataUploadedEventConsumer>();
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
                    e.ConfigureConsumer<FileAndMetadataUploadedEventConsumer>(registrationContext);
                });
                
                
                factory.ConfigureEndpoints(registrationContext);
            });
        });
        services.AddCors();
        services.AddScoped<IMessageFactory, SampleMessageFactory>();
    }
    
}