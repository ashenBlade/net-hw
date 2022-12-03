namespace MessagesAPI.Models;

public record Support(string Username, string UserId) : ConnectedUser(Username, UserId);