using FurAniJoGa.RabbitMq.Contracts.Events;
using FurAniJoGa.Worker.MongoUpdater.FileUploaderCounterService;
using MassTransit;

namespace FurAniJoGa.Worker.MongoUpdater.Consumers;

public class FileUploadedEventConsumer: IConsumer<FileUploadedEvent>
{
    private readonly IFileUploaderCounterService _counter;
    private readonly ILogger<FileUploadedEventConsumer> _logger;

    public FileUploadedEventConsumer(IFileUploaderCounterService counter, ILogger<FileUploadedEventConsumer> logger)
    {
        _counter = counter;
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<FileUploadedEvent> context)
    {
        _logger.LogInformation("File {FileId} was uploaded from request {RequestId}", context.Message.FileId, context.Message.RequestId);
        var newValue = await _counter.IncrementAsync(context.Message.RequestId);
        if (newValue == 2)
        {
            try
            {
                await context.Publish(new FileAndMetadataUploadedEvent() {RequestId = context.Message.RequestId});
            }
            catch (PublishException e)
            {
                _logger.LogWarning(e, "Error occured during FileAndMetadataUploadedEvent publishing for request {RequestId}", context.Message.RequestId);
            }
        }
    }
}