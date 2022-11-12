namespace FurAniJoGa.Worker.MongoUpdater.FileMoveService;

public interface IFileMoveService
{
    Task MoveToPersistentBucketAsync(Guid fileId, CancellationToken token = default);
}