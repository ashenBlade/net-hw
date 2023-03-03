using Microsoft.AspNetCore.Identity;

namespace GrpcChat.Domain;

public class User: IdentityUser<Guid>
{ }