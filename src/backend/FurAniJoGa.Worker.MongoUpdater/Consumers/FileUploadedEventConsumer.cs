using FurAniJoGa.RabbitMq.Contracts.Events;
using FurAniJoGa.Worker.MongoUpdater.FileIdRepository;
using FurAniJoGa.Worker.MongoUpdater.FileUploaderCounterService;
using MassTransit;

namespace FurAniJoGa.Worker.MongoUpdater.Consumers;

public class FileUploadedEventConsumer: IConsumer<FileUploadedEvent>
{
    private readonly IFileUploaderCounterService _counter;
    private readonly IUploadRequestRepository _uploadRequestRepository;
    private readonly ILogger<FileUploadedEventConsumer> _logger;

    public FileUploadedEventConsumer(IFileUploaderCounterService counter,
                                     IUploadRequestRepository uploadRequestRepository, 
                                     ILogger<FileUploadedEventConsumer> logger)
    {
        _counter = counter;
        _uploadRequestRepository = uploadRequestRepository;
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<FileUploadedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("File {FileId} was uploaded from request {RequestId}", message.FileId, message.RequestId);
        var newValue = await _counter.IncrementAsync(message.RequestId);
        if (newValue == 2)
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
                _logger.LogWarning(publishException, "Error occured during FileAndMetadataUploadedEvent publishing for request {RequestId}", message.RequestId);
            }
        }
    }
}