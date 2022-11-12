using System.Net.Http.Json;

namespace FurAniJoGa.RabbitMq.Contracts.Events;

public class MetadataUploadedEvent
{
    public Guid RequestId { get; init; }
    public Dictionary<string,string> Metadata { get; init; } = null!;
}