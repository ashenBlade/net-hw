namespace FurAniJoGa.Domain;

public interface IMessageFactory
{
    Task<Message> CreateMessageAsync(string content, string? username, CancellationToken token = default);
    
    Task<List<Message>> GetMessages(int page, int size, bool fromEnd);
}