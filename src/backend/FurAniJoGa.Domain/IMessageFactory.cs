namespace FurAniJoGa.Domain;

public interface IMessageFactory
{
    Task<Message> CreateMessageAsync(string content, string? username, CancellationToken token = default);
}