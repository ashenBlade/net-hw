namespace FurAniJoGa.Worker.MongoUpdater.FileInfoRepository;

public interface IFileInfoRepository
{
    Task SaveFileAsync(Guid fileId, Dictionary<string, object> metadata, CancellationToken token = default);
}