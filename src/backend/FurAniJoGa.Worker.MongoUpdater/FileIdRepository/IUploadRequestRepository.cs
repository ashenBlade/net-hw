namespace FurAniJoGa.Worker.MongoUpdater.FileIdRepository;

public interface IUploadRequestRepository
{
    Task<Tuple<Guid?, Dictionary<string, object>?>?> FindFileIdAsync(Guid requestId);
}