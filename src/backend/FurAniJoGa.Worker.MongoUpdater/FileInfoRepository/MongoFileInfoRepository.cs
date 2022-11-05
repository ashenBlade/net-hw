using MongoDB.Driver;

namespace FurAniJoGa.Worker.MongoUpdater.FileInfoRepository;

public class MongoFileInfoRepository: IFileInfoRepository
{
    private readonly MongoSettings _settings;
    private readonly IMongoClient _client;
    private IMongoDatabase Database => _client.GetDatabase(_settings.Database);
    private IMongoCollection<MongoFile> MongoFileCollection => Database.GetCollection<MongoFile>(_settings.Collection);
    public MongoFileInfoRepository(MongoSettings settings)
    {
        _settings = settings;
        _client = new MongoClient(new MongoClientSettings()
                                  {
                                      Server = new MongoServerAddress(_settings.Host, _settings.Port),
                                      Credential =
                                          MongoCredential.CreateCredential(_settings.Database, _settings.Username,
                                                                           _settings.Password)
                                  });
    }
    
    public async Task SaveFileAsync(Guid fileId, Dictionary<string, object> metadata, CancellationToken token = default)
    {
        var file = new MongoFile() {FileId = fileId.ToString(), Metadata = metadata};
        await MongoFileCollection.InsertOneAsync(file, null, token);
    }

}