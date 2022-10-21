using FurAniJoGa.WebHost.FileAPI.MetadataUpdater.Abstractions;
using MetadataExtractor;
using MetadataExtractor.Util;

namespace FurAniJoGa.WebHost.FileAPI.MetadataUpdater.Services;

public class MetadataExtractor: IMetadataExtractor
{
    public Dictionary<string, string> ExtractMetadata(Stream stream)
    {
        var metadata = ImageMetadataReader.ReadMetadata(stream).ToList();
        return metadata.Where(d => !( d.HasError || d.IsEmpty ))
                       .SelectMany(d => d.Tags
                                         .Select(t => ( d, t )))
                       .ToDictionary(tuple => $"{tuple.d.Name}:{tuple.t.Name}", 
                                     tuple => tuple.t.Description ?? string.Empty);

    }
}