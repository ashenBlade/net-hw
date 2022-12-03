using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace MessagesAPI.Models;

public record Chat(User User,
                   Support Support, 
                   string ChatId)
{
    public override int GetHashCode()
    {
        return HashCode.Combine(User.UserId, Support.UserId, ChatId);
    }
    
    
}