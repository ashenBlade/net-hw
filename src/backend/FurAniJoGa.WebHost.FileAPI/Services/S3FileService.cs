using System.Net;
using Amazon.S3;
using Amazon.S3.Model;

namespace FurAniJoGa.WebHost.FileAPI.Services;

public class S3FileService: IFileService, IDisposable
{
    private const string OriginalFilenameMetadataField = "original-filename";
    private readonly S3FileServiceOptions _options;
    private readonly ILogger<S3FileService> _logger;
    private readonly AmazonS3Client _client;
    
    public S3FileService(S3FileServiceOptions options, ILogger<S3FileService> logger)
    {
        _options = options;
        _logger = logger;
        var config = new AmazonS3Config() {ServiceURL = _options.Host.ToString(), ForcePathStyle = true};
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
        request.Metadata.Add(OriginalFilenameMetadataField, encodedFilename);
        PutObjectResponse response;
        try
        {
            response = await _client.PutObjectAsync(request, token);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Could not save file. Error occured during PutObjectAsync method call");
            throw;
        }

        if (( int ) response.HttpStatusCode < 300) 
            return id;
        
        _logger.LogWarning("Response from PutObjectAsync returned not success status code: {StatusCode}", response.HttpStatusCode);
        throw new Exception($"Response from PutObjectAsync returned not success status code");
    }

    public async Task<FileContent?> DownloadFileAsync(Guid fileId, CancellationToken token = default)
    {
        var request = new GetObjectRequest()
                      {
                          Key = fileId.ToString(),
                          BucketName = _options.Bucket
                      };
        GetObjectResponse response;
        try
        {
            response = await _client.GetObjectAsync(request, token);
        }
        catch (AmazonS3Exception amazonS3Exception)
        {
            _logger.LogWarning(amazonS3Exception, "Could not get file content by specified key: {FileId}", fileId);
            return null;
        }

        if (response.HttpStatusCode is HttpStatusCode.NotFound)
        {
            return null;
        }

        if ((int)response.HttpStatusCode >= 300)
        {
            _logger.LogWarning("From S3 service returned status code neither 404 nor success: {StatusCode}", 
                               response.HttpStatusCode);
            return null;
        }

        var filename = response.Metadata[OriginalFilenameMetadataField];
        
        return new FileContent()
               {
                   Content = response.ResponseStream,
                   ContentType = response.Headers.ContentType,
                   Filename = filename
               };
    }

    public async Task<File?> GetFileInfoAsync(Guid fileId, CancellationToken token = default)
    {
        var response = await _client.GetObjectMetadataAsync(_options.Bucket, fileId.ToString(), token);
        if ((int)response.HttpStatusCode > 299)
        {
            return null;
        }
        var contentDisposition = response.Headers.ContentDisposition.Split("filename=");
        var filename = contentDisposition.Length > 1
                           ? Uri.UnescapeDataString( contentDisposition[1].Trim('"') )
                           : null;
        
        return new File
               {
                   FileId = fileId,
                   ContentType = response.Headers.ContentType,
                   Filename = filename
               };
    }

    public void Dispose()
    {
        _client.Dispose();
    }
}