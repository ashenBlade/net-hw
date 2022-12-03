using FurAniJoGa.RabbitMq.Contracts.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace MessagesAPI;

public class SupportChatHub : Hub
{
    public const string PublishMessageMethodName = "publishMessage";
    public const string EndChatFunctionName = "endChat";
    
    public const string OnChatStartedFunctionName = "onChatStarted";
    public const string OnChatEndedFunctionName = "onChatEnded";
    

    private readonly IBus _bus;
    private readonly ILogger<SupportChatHub> _logger;

    public SupportChatHub(IBus bus, ILogger<SupportChatHub> logger)
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
        
        _logger.LogInformation("Received message from {Username} with requestId {RequestId}", username, requestId);
        await _bus.Publish(new MessagePublishedEvent
                           {
                               Username = username,
                               Message = message,
                               RequestId = requestId
                           });
        _logger.LogInformation("Event published");
        await Clients.All.SendAsync(PublishMessageMethodName, username, message, requestId);
    }
    
    
}