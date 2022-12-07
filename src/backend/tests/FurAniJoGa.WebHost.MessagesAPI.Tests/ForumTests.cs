using System;
using MessagesAPI.Models;
using Xunit;

namespace FurAniJoGa.WebHost.MessagesAPI.Tests;

public class ForumTests
{
    private readonly Forum _forum = new();

    private static string RandomConnectionId => Guid.NewGuid().ToString();
    
    [Theory]
    [InlineData("user", "support")]
    public void AddSupport_WithExistingUser_ShouldRaiseChatStartedEvent(string username, string support)
    {
        _forum.AddUser(RandomConnectionId, username);
        
        Assert.Raises<ChatEventArgs>(h => _forum.ChatStarted += h, 
                                     h => _forum.ChatStarted -= h,
                                     () => _forum.AddSupport(RandomConnectionId, support));
    }

    [Theory]
    [InlineData("user", "support")]
    public void AddUser_WithExistingSupport_ShouldRaiseChatStartedEvent(string username, string support)
    {
        _forum.AddSupport(RandomConnectionId, support);
        
        Assert.Raises<ChatEventArgs>(h => _forum.ChatStarted += h, 
                                     h => _forum.ChatStarted -= h,
                                     () => _forum.AddUser(RandomConnectionId, username));
    }

    [Theory]
    [InlineData("user", "support")]
    public void DisconnectUser_WithExistingChat_ShouldRaiseChatEndedEvent(string username, string support)
    {
        _forum.AddUser(RandomConnectionId, username);
        var supportConnectionId = RandomConnectionId;
        _forum.AddSupport(supportConnectionId, support);
        
        Assert.Raises<ChatEventArgs>(h => _forum.ChatEnded += h, 
                                     h => _forum.ChatEnded -= h,
                                     () => _forum.DisconnectUser(supportConnectionId));
    }

    [Theory]
    [InlineData("user", "support")]
    public void AddUser_AfterOldUserDisconnect_ShouldStartNewChatWithOldSupport(string username, string support)
    {
        _forum.AddSupport(RandomConnectionId, support);
        var oldUserConnectionId = RandomConnectionId;
        _forum.AddUser(oldUserConnectionId, username);
        _forum.DisconnectUser(oldUserConnectionId);
        Assert.Raises<ChatEventArgs>(h => _forum.ChatStarted += h,
                                     h => _forum.ChatStarted -= h,
                                     () => _forum.AddUser(RandomConnectionId, username));
    }

    [Theory]
    [InlineData("user", "support")]
    public void AddSupport_AfterOldSupportDisconnect_ShouldStartNewChat(string username, string support)
    {
        var supportConnectionId = RandomConnectionId;
        _forum.AddSupport(supportConnectionId, support);
        _forum.AddUser(RandomConnectionId, username);
        _forum.DisconnectUser(supportConnectionId);
        Assert.Raises<ChatEventArgs>(h => _forum.ChatStarted += h,
                                     h => _forum.ChatStarted -= h,
                                     () => _forum.AddSupport(RandomConnectionId, support));
    }
}