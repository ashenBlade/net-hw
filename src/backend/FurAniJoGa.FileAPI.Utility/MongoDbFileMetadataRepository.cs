using FurAniJoGa.FileAPI.Abstractions;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace FurAniJoGa.FileAPI.Utility;

public class MongoDbFileMetadataRepository: IFileMetadataRepository
{
    private readonly MongoClient _client;
    private readonly MongoDbFileMetadataRepositoryOptions _options;
    private readonly ILogger<MongoDbFileMetadataRepository> _logger;

    private IMongoDatabase Database => _client.GetDatabase(_options.Database);
    private IMongoCollection<FileMetadata> FileMetadata => Database.GetCollection<FileMetadata>(_options.Collection);

    public MongoDbFileMetadataRepository(MongoClient client, MongoDbFileMetadataRepositoryOptions options, ILogger<MongoDbFileMetadataRepository> logger)
    {
        _client = client;
        _options = options;
        _logger = logger;
    }

    public async Task<FileMetadata?> GetMetadataByIdAsync(Guid fileId, CancellationToken token = default)
    {
        _logger.LogInformation("Requested search {Id}", fileId);
        using var cursor = await FileMetadata.FindAsync(f => f.FileId == fileId, 
                                                        new FindOptions<FileMetadata>()
                                                        {
                                                            Limit = 1
                                                        },
                                                        token);
        var data = await cursor.SingleOrDefaultAsync(token);
        if (data is null)
        {
            _logger.LogInformation("File {Id} not found", fileId);
        }
        return data;
    }

    public async Task AddMetadataAsync(FileMetadata metadata, 
                                       CancellationToken token = default)
    {
        await FileMetadata.InsertOneAsync(metadata, null, token);
    }
}