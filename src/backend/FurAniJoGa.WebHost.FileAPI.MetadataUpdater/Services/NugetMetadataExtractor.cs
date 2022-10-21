using FurAniJoGa.WebHost.FileAPI.MetadataUpdater.Abstractions;

namespace FurAniJoGa.WebHost.FileAPI.MetadataUpdater.Services;

public class NugetMetadataExtractor: IMetadataExtractor
{
    public Task<Dictionary<string, string>> ExtractMetadata()
    {
        throw new NotImplementedException();
    }
}