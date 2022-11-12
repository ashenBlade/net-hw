using FurAniJoGa.Worker.MongoUpdater.Commands;
using FurAniJoGa.Worker.MongoUpdater.FileMoveService;
using MediatR;

namespace FurAniJoGa.Worker.MongoUpdater.Handlers;

public class SaveToPersistentBucketCommandHandler: IRequestHandler<MoveToPersistentBucketCommand>
{
    private readonly IFileMoveService _fileMoveService;
    private readonly ILogger<SaveToPersistentBucketCommandHandler> _logger;

    public SaveToPersistentBucketCommandHandler(IFileMoveService fileMoveService, 
                                                ILogger<SaveToPersistentBucketCommandHandler> logger)
    {
        _fileMoveService = fileMoveService;
        _logger = logger;
    }
    
    public async Task<Unit> Handle(MoveToPersistentBucketCommand request, CancellationToken cancellationToken)
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
         return Unit.Value;
    }
}