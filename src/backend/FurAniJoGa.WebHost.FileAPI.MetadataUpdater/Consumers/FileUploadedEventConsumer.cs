using FurAniJoGa.FileAPI.Abstractions;
using FurAniJoGa.RabbitMq.Contracts.Events;
using FurAniJoGa.WebHost.FileAPI.MetadataUpdater.Abstractions;
using MassTransit;

namespace FurAniJoGa.WebHost.FileAPI.MetadataUpdater.Consumers;

public class FileUploadedEventConsumer: IConsumer<FileUploadedEvent>
{
    private readonly IFileMetadataRepository _repository;
    private readonly IFileService _fileService;
    private readonly IMetadataExtractor _extractor;
    private readonly ILogger<FileUploadedEventConsumer> _logger;

    public FileUploadedEventConsumer(IFileMetadataRepository repository,
                                     IFileService fileService,
                                     IMetadataExtractor extractor,
                                     ILogger<FileUploadedEventConsumer> logger)
    {
        _repository = repository;
        _fileService = fileService;
        _extractor = extractor;
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<FileUploadedEvent> context)
    {
        var message = context.Message;
        _logger.LogInformation("FileUploadedEvent fired for {FileId}", message.FileId);
        try
        {
            var file = await _fileService.DownloadFileAsync(message.FileId);
            if (file is null)
            {
                _logger.LogWarning("Could not download file with provided File Id: {FileId}", message.FileId);
                return;
            }
            var metadata = await _extractor.ExtractMetadata(file.Content);
            await _repository.AddMetadataAsync(new FileMetadata()
                                         {
                                             Metadata = metadata,
                                             FileId = message.FileId
                                         });
            _logger.LogInformation("File metadata added");
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "Error occured during metadata uploading");
            throw;
        }
    }
}