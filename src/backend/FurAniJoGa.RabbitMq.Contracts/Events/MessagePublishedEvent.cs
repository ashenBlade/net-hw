namespace FurAniJoGa.RabbitMq.Contracts.Events;

public class MessagePublishedEvent
{
    public string Username { get; set; }
    public string Message { get; set; }
    public Guid? AttachmentRequestId { get; set; }
    public Guid? RequestId { get; set; }
}