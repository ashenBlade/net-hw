using FurAniJoGa.Infrastructure;
using MassTransit;
using MessagesListener.Events;

namespace MessagesListener;

public class RabbitMqListener : BackgroundService
{
    private readonly ILogger<RabbitMqListener> _logger;
    private readonly IServiceProvider _provider;

    public RabbitMqListener(ILogger<RabbitMqListener> logger, IServiceProvider provider)
    {
        _logger = logger;
        _provider = provider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("RabbitMQ listener is starting");
        
        await using (var context = _provider.GetRequiredService<MessagesDbContext>())
        {
            await context.Database.EnsureCreatedAsync(stoppingToken);
        }
        
        try
        {
            _logger.LogInformation("RabbitMQ listener begins listening events");
            
            // Run infinitely
            await Task.Delay(-1, stoppingToken);
        }
        catch (TaskCanceledException)
        { }
        finally
        {
            _logger.LogInformation("RabbitMQ listener is stopping");
        }
    }
}