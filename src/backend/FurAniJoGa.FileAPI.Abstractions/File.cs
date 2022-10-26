namespace FurAniJoGa.WebHost.FileAPI;

public class File
{
    public Guid FileId { get; init; }
    public string? Filename { get; init; }
    public string ContentType { get; init; } = null!;
}