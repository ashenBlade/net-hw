using FurAniJoGa.RabbitMq.Contracts.Events;
using FurAniJoGa.Worker.MongoUpdater.FileIdRepository;
using FurAniJoGa.Worker.MongoUpdater.FileUploaderCounterService;
using MassTransit;

namespace FurAniJoGa.Worker.MongoUpdater.Consumers;

public class MetadataUploadedEventConsumer: IConsumer<MetadataUploadedEvent>
{
    private readonly IFileUploaderCounterService _counter;
    private readonly ILogger<MetadataUploadedEventConsumer> _logger;

    public MetadataUploadedEventConsumer(IFileUploaderCounterService counter, ILogger<MetadataUploadedEventConsumer> logger)
    {
        _counter = counter;
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<MetadataUploadedEvent> context)
    {
        _logger.LogInformation("Metadata uploaded event start consuming for request {RequestId}", context.Message.RequestId);
        var state = await _counter.IncrementAsync(context.Message.RequestId);
        if (state == 2)
        {
            try
            {
                await context.Publish(new FileAndMetadataUploadedEvent() {RequestId = context.Message.RequestId}, 
                                      context.CancellationToken);
            }
            catch (PublishException e)
            {
                _logger.LogWarning(e, 
                                   "Error during FileAndMetadataUploadedEvent publishing for request {RequestId}", 
                                   context.Message.RequestId);
            }
        }
    }
}