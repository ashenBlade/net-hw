namespace MessagesAPI.Models;

public class Forum
{
    private readonly Dictionary<string, string> _connectionIdUsername = new();
    
    private readonly HashSet<string> _freeUserIds = new();
    private readonly HashSet<string> _freeSupportIds = new();
    
    
    private readonly Dictionary<string, Chat> _connectionIdChat = new();

    public event ChatEventHandler? ChatStarted;
    public event ChatEventHandler? ChatEnded;
    
    
    public void AddUser(string connectionId, string username)
    {
        if (_connectionIdUsername.ContainsKey(connectionId))
        {
            return;
        }

        _connectionIdUsername[connectionId] = username;
        _freeUserIds.Add(connectionId);
        
        UpdateChats();
    }

    private void UpdateChats()
    {
        if (_freeSupportIds.Count <= 0 || _freeUserIds.Count <= 0) 
            return;
        
        var freeSupportId = _freeSupportIds.First();
        var freeUserId = _freeUserIds.First();

        var user = new User(_connectionIdUsername[freeUserId], freeUserId);
        var support = new Support(_connectionIdUsername[freeSupportId], freeSupportId);
        var chatId = Guid.NewGuid().ToString();
        var chat = new Chat(user,
                            support, 
                            chatId);
            
        _connectionIdChat[freeSupportId] = chat;
        _connectionIdChat[freeUserId] = chat;
            
        _freeSupportIds.Remove(freeSupportId);
        _freeUserIds.Remove(freeUserId);
            
        OnChatStarted(new ChatEventArgs()
                      {
                          ChatId = chatId,
                          SupportConnectionId = freeSupportId,
                          UserConnectionId = freeUserId
                      });
    }
    
    public void AddSupport(string connectionId, string username)
    {
        if (_connectionIdUsername.ContainsKey(connectionId))
        {
            return;
        }
        

        _connectionIdUsername[connectionId] = username;
        _freeSupportIds.Add(connectionId);
        
        UpdateChats();
    }
    
    public void DisconnectUser(string connectionId)
    {
        if (!_connectionIdUsername.Remove(connectionId, out var username))
        {
            // Подключения нет
            return;
        }


        if (!_connectionIdChat.Remove(connectionId, out var chat))
        {
            // Не состоит ни в одном в чате
            return;
        }
        

        if (chat.User.UserId == connectionId)
        {
            _freeSupportIds.Add(chat.Support.UserId);
        }
        else
        {
            _freeUserIds.Add(chat.User.UserId);
        }
        
        
        OnChatEnded(new ChatEventArgs()
                    {
                        ChatId = chat.ChatId,
                        SupportConnectionId = chat.Support.UserId,
                        UserConnectionId = chat.User.UserId
                    });
    }

    public void EndChatForUser(string connectionId)
    {
        if (!_connectionIdChat.Remove(connectionId, out var chat))
        {
            return;
        }

        _freeSupportIds.Add(chat.Support.UserId);
        _freeUserIds.Add(chat.User.UserId);
        
        OnChatEnded(new ChatEventArgs()
                    {
                        ChatId = chat.ChatId,
                        SupportConnectionId = chat.Support.UserId,
                        UserConnectionId = chat.User.UserId
                    });
    }

    public string? FindGroupIdByConnectionId(string connectionId)
    {
        return _connectionIdChat.TryGetValue(connectionId, out var chat)
                   ? chat.ChatId
                   : null;
    }

    public string? GetUsername(string connectionId)
    {
        _connectionIdUsername.TryGetValue(connectionId, out var username);
        return username;
    }

    protected virtual void OnChatEnded(ChatEventArgs args)
    {
        ChatEnded?.Invoke(args);
    }

    protected virtual void OnChatStarted(ChatEventArgs args)
    {
        ChatStarted?.Invoke(args);
    }
}