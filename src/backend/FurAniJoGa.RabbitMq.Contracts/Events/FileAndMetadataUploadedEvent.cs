namespace FurAniJoGa.RabbitMq.Contracts.Events;

public class FileAndMetadataUploadedEvent
{
    public Guid RequestId { get; set; }
    public Guid FileId { get; set; }
    public Dictionary<string, object> Metadata { get; set; }
}