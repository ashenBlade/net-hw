using System.Net.Http.Json;

namespace FurAniJoGa.RabbitMq.Contracts.Events;

public class MetadataUploadedEvent
{
    public Guid RequestId { get; init; }
    public JsonContent Metadata { get; init; } = null!;
}