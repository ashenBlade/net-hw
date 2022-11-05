namespace FurAniJoGa.Worker.MongoUpdater.FileUploaderCounterService;

public interface IFileUploaderCounterService
{
    Task<int> IncrementAsync(Guid requestId);
}