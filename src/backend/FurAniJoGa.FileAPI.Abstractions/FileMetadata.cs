namespace FurAniJoGa.FileAPI.Abstractions;

public class FileMetadata
{
    public Guid FileId { get; set; }
    public Dictionary<string, string> Metadata { get; set; }
}