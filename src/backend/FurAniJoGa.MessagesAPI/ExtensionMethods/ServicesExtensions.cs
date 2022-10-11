using System.Text.Json;
using FurAniJoGa.Domain;
using FurAniJoGa.Infrastructure;
using FurAniJoGa.Infrastructure.Repositories;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace MessagesAPI.ExtensionMethods;

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
        services.AddSignalR();

        services.AddMassTransit(configurator =>
        {
            throw new NotImplementedException("Implement connection to MassTransit pls");
        });
        services.AddScoped<IMessageFactory, SampleMessageFactory>();
    }
    
}