namespace FurAniJoGa.RabbitMq.Contracts.Events;

public class FileUploadedEvent
{
    public string Username { get; init; } = null!;
    public Guid FileId { get; init; }
}