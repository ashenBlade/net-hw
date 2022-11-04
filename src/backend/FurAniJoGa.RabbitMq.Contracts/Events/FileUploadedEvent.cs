namespace FurAniJoGa.RabbitMq.Contracts.Events;

public class FileUploadedEvent
{
    public Guid RequestId { get; set; }
    public Guid FileId { get; set; }
}