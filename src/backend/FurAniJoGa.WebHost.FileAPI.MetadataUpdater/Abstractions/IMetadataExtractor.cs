namespace FurAniJoGa.WebHost.FileAPI.MetadataUpdater.Abstractions;

public interface IMetadataExtractor
{
    Task<Dictionary<string, string>> ExtractMetadata();
}