namespace FurAniJoGa.Worker.MongoUpdater.FileInfoRepository;

public interface IFileMetadataRepository
{
    Task SaveFileAsync(Guid fileId, Dictionary<string, object> metadata, CancellationToken token = default);
}