using System.Net;
using Amazon.Runtime.Internal;
using Amazon.S3;
using Amazon.S3.Model;

namespace FurAniJoGa.WebHost.FileAPI;

public class S3FileService: IFileService
{
    private readonly S3FileServiceOptions _options;
    private readonly ILogger<S3FileService> _logger;
    private readonly AmazonS3Client _client;
    
    public S3FileService(S3FileServiceOptions options, ILogger<S3FileService> logger)
    {
        _options = options;
        _logger = logger;
        var config = new AmazonS3Config() {ServiceURL = _options.Host.ToString(), ForcePathStyle = true,};
        _client = new AmazonS3Client(options.SecretKey, options.Password, config);
    }
    
    public async Task<Guid> SaveFileAsync(IFormFile file, CancellationToken token = default)
    {
        var id = Guid.NewGuid();
        await using var stream = file.OpenReadStream();
        var encodedFilename = Uri.EscapeDataString(file.FileName);
        var request = new PutObjectRequest()
                      {
                          BucketName = _options.Bucket,
                          InputStream = stream,
                          AutoCloseStream = true,
                          Key = id.ToString(),
                          ContentType = file.ContentType,
                          Headers =
                          {
                              ContentDisposition = $"attachment; filename=\"{encodedFilename}\""
                          },
                          
                      };
        request.Metadata.Add("original-filename", encodedFilename);
        try
        {
            var response = await _client.PutObjectAsync(request, token);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Could not save file. Error occured during PutObjectAsync method call");
            throw;
        }
        return id;
    }

    public async Task<File?> DownloadFileAsync(Guid fileId, CancellationToken token = default)
    {
        var request = new GetObjectRequest()
                      {
                          Key = fileId.ToString(),
                          BucketName = _options.Bucket
                      };
        var response = await _client.GetObjectAsync(request, token);
        if (response.HttpStatusCode is HttpStatusCode.NotFound)
        {
            return null;
        }

        var contentDisposition = response.Headers.ContentDisposition.Split("filename=");
        var filename = contentDisposition.Length > 1
                           ? contentDisposition[1].Trim('"')
                           : null;
        
        return new File()
               {
                   Stream = response.ResponseStream,
                   ContentType = response.Headers.ContentType,
                   Filename = filename
               };
    }
}