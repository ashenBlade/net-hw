using Microsoft.EntityFrameworkCore;

namespace MessagesAPI.Models;

public class Forum
{
    private readonly Dictionary<string, string> _connectionIdUsername = new();
    
    private Queue<string> _freeUserIds = new();
    private Queue<string> _freeSupportIds = new();
    
    
    private readonly Dictionary<string, Chat> _connectionIdChat = new();
    
    public event EventHandler<ChatEventArgs>? ChatStarted;
    public event EventHandler<ChatEventArgs>? ChatEnded;
    
    
    public void AddUser(string connectionId, string username)
    {
        if (_connectionIdUsername.ContainsKey(connectionId))
        {
            return;
        }

        _connectionIdUsername[connectionId] = username;
        _freeUserIds.Enqueue(connectionId);

        if (_freeSupportIds.Count == 0)
        {
            return;
        }

        var freeSupportId = _freeSupportIds.Dequeue();
        var chatId = Guid.NewGuid().ToString();
        var chat = new Chat(new User(username, connectionId), 
                            new Support(_connectionIdUsername[freeSupportId], freeSupportId), 
                            chatId);
        _connectionIdChat[freeSupportId] = _connectionIdChat[connectionId] = chat;
        
        OnChatStarted(new ChatEventArgs()
                      {
                          ChatId = chatId,
                          SupportConnectionId = freeSupportId,
                          UserConnectionId = connectionId
                      });
    }

    public void ForceUpdateChats()
    {
        if (_freeSupportIds.Count == 0 || _freeUserIds.Count == 0)
        {
            return;
        }

        var freeSupportId = _freeSupportIds.Dequeue();
        var freeUserId = _freeUserIds.Dequeue();

        var user = new User(_connectionIdUsername[freeUserId], freeUserId);
        var support = new Support(_connectionIdUsername[freeSupportId], freeSupportId);
        var chatId = Guid.NewGuid().ToString();
        var chat = new Chat(user,
                            support, 
                            chatId);
            
        _connectionIdChat[freeSupportId] = _connectionIdChat[freeUserId] = chat;
            
        OnChatStarted(new ChatEventArgs()
                      {
                          ChatId = chatId,
                          SupportConnectionId = freeSupportId,
                          UserConnectionId = freeUserId
                      });
    }
    
    public void AddSupport(string supportConnectionId, string username)
    {
        if (_connectionIdUsername.ContainsKey(supportConnectionId))
        {
            return;
        }
        

        _connectionIdUsername[supportConnectionId] = username;
        _freeSupportIds.Enqueue(supportConnectionId);
        
        if (_freeUserIds.Count == 0)
        {
            return;
        }

        var freeUserId = _freeUserIds.Dequeue();
        var user = new User(_connectionIdUsername[freeUserId], freeUserId);
        var support = new Support(username, supportConnectionId);
        var chatId = Guid.NewGuid().ToString();
        var chat = new Chat(user, support, chatId);
        _connectionIdChat[freeUserId] = _connectionIdChat[supportConnectionId] = chat;
        OnChatStarted(new ChatEventArgs()
                      {
                          ChatId = chatId,
                          SupportConnectionId = supportConnectionId,
                          UserConnectionId = freeUserId
                      });
    }

    private static Queue<string> RemoveFrom(Queue<string> original, string item)
    {
        var removed = new Queue<string>(original.Where(i => i != item));
        return removed;
    }

    public void DisconnectUser(string connectionId)
    {
        if (!_connectionIdUsername.Remove(connectionId, out var username))
        {
            // Подключения нет
            return;
        }

        _freeSupportIds = RemoveFrom(_freeSupportIds, connectionId);
        _freeUserIds = RemoveFrom(_freeUserIds, connectionId);
        
        if (!_connectionIdChat.Remove(connectionId, out var chat))
        {
            // Не состоит ни в одном в чате
            return;
        }
        
        if (chat.User.UserId == connectionId)
        {
            _connectionIdChat.Remove(chat.Support.UserId);
            _freeSupportIds.Enqueue(chat.Support.UserId);
        }
        else
        {
            _connectionIdChat.Remove(chat.User.UserId);
            _freeUserIds.Enqueue(chat.User.UserId);
        }
        
        
        OnChatEnded(new ChatEventArgs()
                    {
                        ChatId = chat.ChatId,
                        SupportConnectionId = chat.Support.UserId,
                        UserConnectionId = chat.User.UserId
                    });
        
        ForceUpdateChats();
    }

    public void EndChatForUser(string connectionId)
    {
        if (!_connectionIdChat.Remove(connectionId, out var chat))
        {
            return;
        }

        _freeSupportIds.Enqueue(chat.Support.UserId);
        _freeUserIds.Enqueue(chat.User.UserId);
        
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

    public (string chatId, string userId, string supportId)? FindChatByConnectionId(string connectionId)
    {
        return _connectionIdChat.TryGetValue(connectionId, out var chat)
                   ? ( chat.ChatId, chat.User.UserId, chat.Support.UserId )
                   : null;
    }

    public string? GetUsername(string connectionId)
    {
        _connectionIdUsername.TryGetValue(connectionId, out var username);
        return username;
    }

    protected virtual void OnChatEnded(ChatEventArgs args)
    {
        ChatEnded?.Invoke(this, args);
    }

    protected virtual void OnChatStarted(ChatEventArgs args)
    {
        ChatStarted?.Invoke(this, args);
    }
}