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
        var e = context.Message;
        _logger.LogInformation("Metadata uploaded event start consuming for request {RequestId}", e.RequestId);
        var state = await _counter.IncrementAsync(e.RequestId);
        if (state == 2)
        {
            var result = await _uploadRequestRepository.FindFileIdAsync(e.RequestId);
            if (result is null)
            {
                _logger.LogWarning("Could not find file id and metadata by provided request id ({RequestId}) in event", e.RequestId);
                return;
            }

            var (fileId, metadata) = result;
            try
            {
                await context.Publish(new FileAndMetadataUploadedEvent
                                      {
                                          RequestId = e.RequestId,
                                          FileId = fileId,
                                          Metadata = metadata
                                      }, 
                                      context.CancellationToken);
            }
            catch (PublishException publishException)
            {
                _logger.LogWarning(publishException, 
                                   "Error during FileAndMetadataUploadedEvent publishing for request {RequestId}", 
                                   e.RequestId);
            }
        }
    }
}