using FurAniJoGa.WebHost.FileAPI.RedisMetadataUploaderService;
using FurAniJoGa.WebHost.FileAPI.Services;
using MassTransit;

namespace FurAniJoGa.WebHost.FileAPI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFileApi(this IServiceCollection services, 
                                                S3FileServiceOptions s3FileServiceOptions,
                                                RedisSettings redisSettings,
                                                IConfiguration config)
    {
        services.AddControllers();
        services.AddSingleton(s3FileServiceOptions);
        services.AddSingleton(redisSettings);
        services.AddScoped<IFileService, S3FileService>();
        services.AddScoped<IUploaderService, RedisUploaderService>();
        services.AddMassTransit(configurator =>
        {
            configurator.UsingRabbitMq((registrationContext, factory) =>
            {
                var host = config.GetValue<string>("RABBITMQ_HOST");
                var exchange = config.GetValue<string>("RABBITMQ_EXCHANGE");
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

        services.AddCors();

        return services;
    }
}