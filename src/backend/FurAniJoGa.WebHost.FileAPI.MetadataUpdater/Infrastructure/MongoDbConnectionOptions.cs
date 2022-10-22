namespace FurAniJoGa.WebHost.FileAPI.MetadataUpdater.Infrastructure;

public class MongoDbConnectionOptions
{
    public string Host { get; set; } = null!;
    public int Port { get; set; }
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
}