namespace FurAniJoGa.Domain;

public class Message
{
    public Message(Guid id, DateTime publishDate, string? username, string content)
    {
        Content = content ?? throw new ArgumentNullException(nameof(content));
        PublishDate = publishDate;
        Username = username;
        Id = id;
    }
    
    public Message(DateTime publishDate, string? username, string content)
        : this(Guid.NewGuid(), publishDate, username, content) 
    { }

    public Guid Id { get; }
    public DateTime PublishDate { get; }
    public string? Username { get; }
    public string Content { get; }
}