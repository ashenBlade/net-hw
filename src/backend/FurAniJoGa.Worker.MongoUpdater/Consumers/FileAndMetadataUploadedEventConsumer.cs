using FurAniJoGa.RabbitMq.Contracts.Events;
using FurAniJoGa.Worker.MongoUpdater.FileIdRepository;
using FurAniJoGa.Worker.MongoUpdater.FileInfoRepository;
using MassTransit;

namespace FurAniJoGa.Worker.MongoUpdater.Consumers;

public class FileAndMetadataUploadedEventConsumer: IConsumer<FileAndMetadataUploadedEvent>
{
    private readonly IFileInfoRepository _fileInfoRepository;
    private readonly ILogger<FileAndMetadataUploadedEventConsumer> _logger;
    private readonly IUploadRequestRepository _uploadRequestRepository;

    public FileAndMetadataUploadedEventConsumer(IFileInfoRepository fileInfoRepository, ILogger<FileAndMetadataUploadedEventConsumer> logger, IUploadRequestRepository uploadRequestRepository)
    {
        _fileInfoRepository = fileInfoRepository;
        _logger = logger;
        _uploadRequestRepository = uploadRequestRepository;
    }
    
    public async Task Consume(ConsumeContext<FileAndMetadataUploadedEvent> context)
    {
        var requestId = context.Message.RequestId;
        _logger.LogInformation("File and metadata were uploaded for request {RequestId}", requestId);
        Guid fileId;
        Dictionary<string, object> metadata;
        try
        {
            var tuple = await _uploadRequestRepository.FindFileIdAsync(requestId);
            if (tuple is null)
            {
                _logger.LogWarning("No file info found for request {RequestId}", requestId);
                return;
            }

            var (fileIdReturned, metadataReturned) = tuple;
            if (fileIdReturned is null)
            {
                _logger.LogWarning("FileId for request {RequestId} is null", requestId);
            }

            if (metadataReturned is null)
            {
                _logger.LogWarning("Metadata for request {RequestId} is null", requestId);
            }

            if (fileIdReturned is null || metadataReturned is null)
            {
                return;
            }

            fileId = fileIdReturned.Value;
            metadata = metadataReturned;
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "Error occured during file info getting for request {RequestId}", requestId);
            return;
        }

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