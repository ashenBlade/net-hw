namespace FurAniJoGa.Domain.Models;

public class Message
{
    public DateTime PublishDate { get; set; }
    public Guid Id { get; set; }
    public string? Username { get; set; }
    public string Content { get; set; }
    public Guid? FileId { get; set; }

    public Message(Guid id,DateTime publishDate, string name, string content, Guid? fileId)
    {
        Id = id;
        Username = name;
        Content = content;
        PublishDate = publishDate;
        FileId = fileId ?? new Guid();
    }
    
    public Message(DateTime publishDate, string? username, string content, Guid? fileId)
        : this(Guid.NewGuid(), publishDate, username, content, fileId) 
    { }
}