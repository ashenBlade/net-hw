namespace FurAniJoGa.RabbitMq.Contracts.Events;

public class MessagePublishedEvent
{
    public string Username { get; set; }
    public string Message { get; set; }
    public Guid? FileId { get; set; }
}