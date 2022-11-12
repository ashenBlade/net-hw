using FurAniJoGa.RabbitMq.Contracts.Events;
using FurAniJoGa.Worker.MongoUpdater.FileInfoRepository;
using MassTransit;

namespace FurAniJoGa.Worker.MongoUpdater.Consumers;

public class FileAndMetadataUploadedEventConsumer: IConsumer<FileAndMetadataUploadedEvent>
{
    private readonly IFileInfoRepository _fileInfoRepository;
    private readonly ILogger<FileAndMetadataUploadedEventConsumer> _logger;

    public FileAndMetadataUploadedEventConsumer(IFileInfoRepository fileInfoRepository, 
                                                ILogger<FileAndMetadataUploadedEventConsumer> logger)
    {
        _fileInfoRepository = fileInfoRepository;
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<FileAndMetadataUploadedEvent> context)
    {
        var requestId = context.Message.RequestId;
        _logger.LogInformation("File and metadata were uploaded for request {RequestId}", requestId);
        var (fileId, metadata) = ( context.Message.FileId, context.Message.Metadata );
        try
        {
            await _fileInfoRepository.SaveFileAsync(fileId, metadata, context.CancellationToken);
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "Error occured during file info saving to database");
            return;
        }
        _logger.LogInformation("File info for file {FileId} from request {RequestId} saved", fileId, requestId);
    }
}