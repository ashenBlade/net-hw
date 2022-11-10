namespace FurAniJoGa.Domain.Models;

public class Message
{
    public DateTime PublishDate { get; set; }
    public Guid Id { get; set; }
    public string? Username { get; set; }
    public string Content { get; set; }
    public Guid? FileId { get; set; }
    public Guid? RequestId { get; set; }

    public Message(Guid id,DateTime publishDate, string name, string content, Guid? fileId, Guid? requestId)
    {
        Id = id;
        Username = name;
        Content = content;
        PublishDate = publishDate;
        FileId = fileId;
        RequestId = requestId;
    }
    
    public Message(DateTime publishDate, string? username, string content, Guid? fileId, Guid? requestId)
        : this(Guid.NewGuid(), publishDate, username, content, fileId, requestId) 
    { }
}