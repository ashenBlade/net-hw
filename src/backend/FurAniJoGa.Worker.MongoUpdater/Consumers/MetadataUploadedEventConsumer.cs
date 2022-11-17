using FurAniJoGa.RabbitMq.Contracts.Events;
using FurAniJoGa.Worker.MongoUpdater.FileIdRepository;
using FurAniJoGa.Worker.MongoUpdater.FileUploaderCounterService;
using MassTransit;

namespace FurAniJoGa.Worker.MongoUpdater.Consumers;

public class MetadataUploadedEventConsumer: IConsumer<MetadataUploadedEvent>
{
    private readonly IFileUploaderCounterService _counter;
    private readonly IUploadRequestRepository _uploadRequestRepository;
    private readonly ILogger<MetadataUploadedEventConsumer> _logger;

    public MetadataUploadedEventConsumer(IFileUploaderCounterService counter,
                                         IUploadRequestRepository uploadRequestRepository, 
                                         ILogger<MetadataUploadedEventConsumer> logger)
    {
        _counter = counter;
        _uploadRequestRepository = uploadRequestRepository;
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<MetadataUploadedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("Metadata uploaded event start consuming for request {RequestId}", message.RequestId);
        var state = await _counter.IncrementAsync(message.RequestId);
        if (state == 2)
        {
            var result = await _uploadRequestRepository.FindFileIdAsync(message.RequestId);
            if (result is null)
            {
                _logger.LogWarning("Could not find file id and metadata by provided request id ({RequestId}) in event", message.RequestId);
                return;
            }

            var (fileId, metadata) = result;
            try
            {
                await context.Publish(new FileAndMetadataUploadedEvent
                                      {
                                          RequestId = message.RequestId,
                                          FileId = fileId,
                                          Metadata = metadata
                                      }, 
                                      context.CancellationToken);
            }
            catch (PublishException publishException)
            {
                _logger.LogWarning(publishException, 
                                   "Error during FileAndMetadataUploadedEvent publishing for request {RequestId}", 
                                   message.RequestId);
            }
        }
    }
}