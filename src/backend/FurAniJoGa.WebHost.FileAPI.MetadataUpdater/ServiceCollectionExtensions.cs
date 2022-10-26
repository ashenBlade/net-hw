using FurAniJoGa.FileAPI.Abstractions;
using FurAniJoGa.FileAPI.Utility;
using FurAniJoGa.FileAPI.Utility.S3FileService;
using FurAniJoGa.WebHost.FileAPI.MetadataUpdater.Consumers;
using MassTransit;
using MongoDB.Driver;

namespace FurAniJoGa.WebHost.FileAPI.MetadataUpdater;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFileUploadedEventConsumer(this IServiceCollection services,
                                                                  IConfiguration configuration)
    {
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

            configurator.AddConsumer<FileUploadedEventConsumer>();
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
                    e.ConfigureConsumer<FileUploadedEventConsumer>(registrationContext);
                });
                factory.ConfigureEndpoints(registrationContext);
            });
            
        });

        var mongoDbOptions = configuration.GetMongoDbConnectionOptions();
        services.AddSingleton(new MongoUrl($"mongodb://"
                                         + $"{mongoDbOptions.Username}:"
                                         + $"{mongoDbOptions.Password}@"
                                         + $"{mongoDbOptions.Host}:"
                                         + $"{mongoDbOptions.Port}"));
        
        services.AddScoped<IMongoClient, MongoClient>(s => new MongoClient(s.GetRequiredService<MongoUrl>()));
        
        services.AddSingleton(configuration.GetMongoDbFileMetadataRepositoryOptions());
        services.AddScoped<IFileMetadataRepository, MongoDbFileMetadataRepository>();
        
        services.AddScoped<IFileService, S3FileService>();
        

        return services;
    }
}