namespace FurAniJoGa.Domain.Models;

public class Message
{
    public DateTime PublishDate { get; set; }
    public Guid Id { get; set; }
    public string Username { get; set; } = null!;
    public string Content { get; set; } = null!;
    public Guid? RequestId { get; set; }
    public Request? Request { get; set; }

    public Message(Guid id, DateTime publishDate, string name, string content, Request? request = null)
    {
        Id = id;
        Username = name;
        Content = content;
        PublishDate = publishDate;
        Request = request;
        RequestId = request?.Id;
    }

    private Message() { }
    
    public Message(DateTime publishDate, string username, string content, Request? file = null)
        : this(Guid.NewGuid(), publishDate, username, content, file) 
    { }
}