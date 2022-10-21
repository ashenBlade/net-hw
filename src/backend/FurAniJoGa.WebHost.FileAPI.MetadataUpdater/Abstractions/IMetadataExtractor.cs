namespace FurAniJoGa.WebHost.FileAPI.MetadataUpdater.Abstractions;

public interface IMetadataExtractor
{
    Dictionary<string, string> ExtractMetadata(Stream stream);
}