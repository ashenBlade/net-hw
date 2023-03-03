namespace GrpcChat.TokenGenerator;

public interface ITokenGenerator
{
    string GenerateToken(string subject);
}