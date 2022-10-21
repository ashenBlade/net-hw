namespace FurAniJoGa.FileAPI.Utility;

public class MongoDbFileMetadataRepositoryOptions
{
    public string Database { get; init; } = null!;
    public string Collection { get; init; } = null!;
}