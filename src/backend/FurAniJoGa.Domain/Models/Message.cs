namespace FurAniJoGa.Domain.Models;

public class Message
{
    public DateTime PublishDate { get; set; }
    public Guid Id { get; set; }
    public string? Username { get; set; }
    public string Content { get; set; }

    public Message(Guid id,DateTime publishDate, string name, string content)
    {
        Id = id;
        Username = name;
        Content = content;
        PublishDate = publishDate;
    }
    
    public Message(DateTime publishDate, string? username, string content)
        : this(Guid.NewGuid(), publishDate, username, content) 
    { }
}