using Amazon.S3;
using Amazon.S3.Model;

namespace FurAniJoGa.Worker.MongoUpdater.FileMoveService;

public class S3FileMoveService: IFileMoveService, IDisposable
{
    private readonly S3FileMoveServiceOptions _options;
    private readonly ILogger<S3FileMoveService> _logger;
    private readonly AmazonS3Client _client;
    
    public S3FileMoveService(S3FileMoveServiceOptions options, 
                             ILogger<S3FileMoveService> logger)
    {
        _options = options;
        _logger = logger;
        var config = new AmazonS3Config() {ServiceURL = _options.Host.ToString(), ForcePathStyle = true};
        _client = new AmazonS3Client(options.SecretKey, options.Password, config);
    }
    
    public async Task MoveToPersistentBucketAsync(Guid fileId, CancellationToken token = default)
    {
        var fileIdString = fileId.ToString();
        _logger.LogInformation("Requested moving file {FileId} from temp to persistent bucket", fileId);
        try
        {
            _logger.LogInformation("Sending CopyObjectRequest for file {FileId} from temp bucket to persistent", fileIdString);
            await _client.CopyObjectAsync(new CopyObjectRequest()
                                          {
                                              SourceBucket = _options.TemporaryBucketName,
                                              SourceKey = fileIdString,
                          
                                              DestinationBucket = _options.PersistentBucketName,
                                              DestinationKey = fileIdString,
                                          }, token);
            _logger.LogInformation("File {FileId} was copied from temporary bucket to persistent bucket", fileIdString);
        }
        catch (AmazonS3Exception amazonException)
        {
            _logger.LogWarning(amazonException, "Error during file ({FileId}) copy", fileIdString);
            throw;
        }
    }

    public void Dispose()
    {
        _client.Dispose();
    }
}