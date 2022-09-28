namespace FurAniJoGa.Infrastructure.Models;

public class Message
{
    public DateTime PublishDate { get; set; }
    public Guid Id { get; set; }
    public string? Username { get; set; }
    public string Content { get; set; }
}