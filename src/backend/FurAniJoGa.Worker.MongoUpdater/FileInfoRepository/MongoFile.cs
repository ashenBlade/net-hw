using MongoDB.Bson.Serialization.Attributes;

namespace FurAniJoGa.Worker.MongoUpdater.FileInfoRepository;

public class MongoFile
{
    [BsonId]
    public string FileId { get; set; } = default!;
    public Dictionary<string, object> Metadata { get; set; } = default!;
}