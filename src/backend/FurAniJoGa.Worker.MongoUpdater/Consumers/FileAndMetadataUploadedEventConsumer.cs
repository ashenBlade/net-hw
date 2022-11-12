using FurAniJoGa.RabbitMq.Contracts.Events;
using FurAniJoGa.Worker.MongoUpdater.Commands;
using FurAniJoGa.Worker.MongoUpdater.FileInfoRepository;
using MassTransit;
using MediatR;

namespace FurAniJoGa.Worker.MongoUpdater.Consumers;

public class FileAndMetadataUploadedEventConsumer: IConsumer<FileAndMetadataUploadedEvent>
{
    private readonly IFileMetadataRepository _fileMetadataRepository;
    private readonly ILogger<FileAndMetadataUploadedEventConsumer> _logger;
    private readonly IMediator _mediator;

    public FileAndMetadataUploadedEventConsumer(IFileMetadataRepository fileMetadataRepository, 
                                                ILogger<FileAndMetadataUploadedEventConsumer> logger,
                                                IMediator mediator)
    {
        _fileMetadataRepository = fileMetadataRepository;
        _logger = logger;
        _mediator = mediator;
    }
    
    public async Task Consume(ConsumeContext<FileAndMetadataUploadedEvent> context)
    {
        var requestId = context.Message.RequestId;
        _logger.LogInformation("File and metadata were uploaded for request {RequestId}", requestId);
        var (fileId, metadata) = ( context.Message.FileId, context.Message.Metadata );
        
        try
        {
            await _mediator.Publish(new MoveToPersistentBucketCommand() {FileId = fileId});
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "Could not publish MoveToPersistentBucketCommand");
            return;
        }
        try
        {
            await _fileMetadataRepository.SaveFileAsync(fileId, metadata, context.CancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "Error occured during file info saving to database");
            return;
        }
        _logger.LogInformation("File info for file {FileId} from request {RequestId} saved", fileId, requestId);
    }
}