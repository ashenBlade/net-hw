namespace FurAniJoGa.WebHost.FileAPI;

public class FileContent
{
    public string ContentType { get; init; } = null!;
    public Stream Content { get; init; } = null!;
}