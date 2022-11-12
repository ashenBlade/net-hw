namespace FurAniJoGa.Worker.MongoUpdater.FileMoveService;

public class S3FileMoveServiceOptions
{
    public string TemporaryBucketName { get; init; } = null!;
    public string PersistentBucketName { get; init; } = null!;
    public Uri Host { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string SecretKey { get; init; } = null!;
}