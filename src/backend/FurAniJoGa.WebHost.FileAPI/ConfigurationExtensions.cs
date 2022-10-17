namespace FurAniJoGa.WebHost.FileAPI;

public static class ConfigurationExtensions
{
    public static S3FileServiceOptions GetS3FileServiceOptions(this ConfigurationManager manager)
    {
        return new S3FileServiceOptions()
               {
                   Bucket = manager["S3_BUCKET"] ?? throw new ArgumentException("S3_BUCKET env variable is not provided"),
                   Host = new Uri(manager["S3_HOST"]) ?? throw new ArgumentException("S3_HOST env variable is not provided"),
                   Password = manager["S3_PASSWORD"] ?? throw new ArgumentException("S3_PASSWORD env variable is not provided"),
                   SecretKey = manager["S3_SECRET"] ?? throw new ArgumentException("S3_SECRET env variable is not provided")
               };
    }
}