namespace FurAniJoGa.WebHost.FileAPI;

public class S3FileServiceOptions
{
    public string Bucket { get; init; } = null!;
    public Uri Host { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string SecretKey { get; init; } = null!;
}