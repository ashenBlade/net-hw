using FurAniJoGa.Worker.MongoUpdater.Commands;
using FurAniJoGa.Worker.MongoUpdater.FileMoveService;
using MediatR;

namespace FurAniJoGa.Worker.MongoUpdater.Handlers;

public class MoveToPersistentBucketCommandHandler: INotificationHandler<MoveToPersistentBucketCommand>
{
    private readonly IFileMoveService _fileMoveService;
    private readonly ILogger<MoveToPersistentBucketCommandHandler> _logger;

    public MoveToPersistentBucketCommandHandler(IFileMoveService fileMoveService, 
                                                ILogger<MoveToPersistentBucketCommandHandler> logger)
    {
        _fileMoveService = fileMoveService;
        _logger = logger;
    }
    
    public async Task Handle(MoveToPersistentBucketCommand request, CancellationToken cancellationToken)
    {
        var fileId = request.FileId;
        _logger.LogInformation("Saving file ({FileId}) to Persistent bucket command requested", fileId);
         try
         {
             await _fileMoveService.MoveToPersistentBucketAsync(fileId, cancellationToken);
             _logger.LogInformation("File ({FileId}) was moved to Persistent bucket", fileId);
         }
         catch (Exception e)
         {
             _logger.LogWarning(e, "Exception occured during moving file ({FileId}) to persistent bucket", fileId);
         }
    }
}