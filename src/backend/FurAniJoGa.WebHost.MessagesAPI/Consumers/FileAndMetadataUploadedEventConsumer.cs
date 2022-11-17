using FurAniJoGa.Domain;
using FurAniJoGa.RabbitMq.Contracts.Events;
using MassTransit;
using Microsoft.AspNetCore.SignalR;

namespace MessagesAPI.Consumers;

public class FileAndMetadataUploadedEventConsumer : IConsumer<FileAndMetadataUploadedEvent>
{
    public const string EventHandlerFunctionName = "onFileUploaded";
    private readonly IHubContext<ChatHub> _hubContext;
    private readonly ILogger<FileAndMetadataUploadedEventConsumer> _logger;
    private readonly IMessageRepository _messageRepository;

    public FileAndMetadataUploadedEventConsumer(IHubContext<ChatHub> hubContext,
                                                ILogger<FileAndMetadataUploadedEventConsumer> logger,
                                                IMessageRepository messageRepository)
    {
        _hubContext = hubContext;
        _logger = logger;
        _messageRepository = messageRepository;
    }

    public async Task Consume(ConsumeContext<FileAndMetadataUploadedEvent> context)
    {
        var e = context.Message;
        _logger.LogInformation("Received FileAndMetadataUploadedEvent for request: {RequestId}", e.RequestId);

        try
        {
            await _messageRepository.UpdateFileIdInMessageAsync(e.RequestId, e.FileId);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, "No request id: {RequestId} in database", e.RequestId);
        }

        try
        {
            await _hubContext.Clients.All.SendAsync(EventHandlerFunctionName, e.RequestId, e.FileId, e.Metadata);
        }
        catch (HubException ex)
        {
            _logger.LogWarning(ex,
                               $"Error during {EventHandlerFunctionName} function SignalR calling for request: {{RequestId}}",
                               e.RequestId);
        }
    }
}