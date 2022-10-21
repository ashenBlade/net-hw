using FurAniJoGa.FileAPI.Abstractions;
using FurAniJoGa.RabbitMq.Contracts.Events;
using MassTransit;

namespace FurAniJoGa.WebHost.FileAPI.MetadataUpdater.Consumers;

public class FileUploadedEventConsumer: IConsumer<FileUploadedEvent>
{
    private readonly IFileMetadataRepository _repository;
    private readonly ILogger<FileUploadedEventConsumer> _logger;

    public FileUploadedEventConsumer(IFileMetadataRepository repository, 
                                     ILogger<FileUploadedEventConsumer> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public async Task Consume(ConsumeContext<FileUploadedEvent> context)
    {
        _logger.LogInformation("FileUploadedEvent fired for {FileId}", context.Message.FileId);
        try
        {
            await _repository.AddMetadataAsync(new FileMetadata()
                                         {
                                             Metadata = new Dictionary<string, string>(),
                                             FileId = context.Message.FileId
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