namespace GrpcChat.TokenGenerator;

public interface ITokenGenerator
{
    string GenerateToken(string username, string email);
}