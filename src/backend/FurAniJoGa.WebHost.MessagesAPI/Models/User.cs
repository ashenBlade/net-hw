namespace MessagesAPI.Models;

public record User(string Username, string UserId) : ConnectedUser(Username, UserId);