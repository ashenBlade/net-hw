using FurAniJoGa.RabbitMq.Contracts.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace MessagesAPI;

public class ChatHub : Hub
{
    private const string PublishMessageMethodName = "publishMessage";
    
    private readonly IBus _bus;
    private readonly ILogger<ChatHub> _logger;

    public ChatHub(IBus bus, ILogger<ChatHub> logger)
    {
        _bus = bus;
        _logger = logger;
    }

    [EndpointName(PublishMessageMethodName)]
    public async Task PublishMessage(string? username, string? message, Guid? requestId)
    {
        if (username is null)
        {
            _logger.LogWarning("Published message username is null");
            return;
        }
        if (message is null)
        {
            _logger.LogWarning("Published message content is null");
            return;
        }

        if (requestId is null)
        {
            _logger.LogWarning("RequestId is null");
            return;
        }
        
        _logger.LogInformation("Received message from {Username} with requestId {RequestId}", username, requestId);
        await _bus.Publish(new MessagePublishedEvent
                           {
                               Username = username,
                               Message = message,
                               RequestId = requestId
                           });
        await Clients.All.SendAsync(PublishMessageMethodName, username, message, requestId);
    }
}