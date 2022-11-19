using System.Text.Json;
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
    private readonly IRequestRepository _requestRepository;

    public FileAndMetadataUploadedEventConsumer(IHubContext<ChatHub> hubContext,
                                                ILogger<FileAndMetadataUploadedEventConsumer> logger,
                                                IRequestRepository requestRepository)
    {
        _hubContext = hubContext;
        _logger = logger;
        _requestRepository = requestRepository;
    }

    public async Task Consume(ConsumeContext<FileAndMetadataUploadedEvent> context)
    {
        var e = context.Message;
        _logger.LogInformation("Received FileAndMetadataUploadedEvent for request: {RequestId}", e.RequestId);

        await _requestRepository.UpsertFileIdAsync(e.RequestId, e.FileId);
        
        try
        {
            await _hubContext.Clients.All.SendAsync(EventHandlerFunctionName, e.RequestId.ToString(), e.FileId.ToString(), JsonSerializer.Serialize( e.Metadata ));
        }
        catch (HubException ex)
        {
            _logger.LogWarning(ex,
                               $"Error during {EventHandlerFunctionName} function SignalR calling for request: {{RequestId}}",
                               e.RequestId);
        }
    }
}