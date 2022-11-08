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


    public async Task NotifyFileUploadedAsync(Guid requestId, Guid fileId, Dictionary<string, object> metadata, CancellationToken token = default)
    {
        try
        {
            await Clients.All.SendAsync("onFileUploaded", requestId, fileId, metadata, token);
        }
        catch (HubException hubException)
        {
            _logger.LogWarning(hubException, "Could not notify clients of file uploaded event");
        }
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

        await _bus.Publish(new MessagePublishedEvent {Username = username, Message = message, AttachmentRequestId = requestId});
        await Clients.All.SendAsync(PublishMessageMethodName, username, message, requestId);
    }
}