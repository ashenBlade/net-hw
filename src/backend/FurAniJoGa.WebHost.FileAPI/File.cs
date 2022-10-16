namespace FurAniJoGa.WebHost.FileAPI;

public class File
{
    public Stream Stream { get; set; } = null!;
    public string? Filename { get; set; }
    public string ContentType { get; set; } = null!;
}