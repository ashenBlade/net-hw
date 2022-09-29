namespace FurAniJoGa.Domain;

public interface IMessageManager
{
    Task<List<Message>> GetMessages(int page, int size, bool fromEnd);
}