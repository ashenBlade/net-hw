﻿using GrpcChat.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GrpcChat.Database;

public sealed class ChatDbContext : IdentityDbContext<User, Role, Guid>
{
    public ChatDbContext(DbContextOptions<ChatDbContext> options)
        :base(options)
    { 
        Database.EnsureCreated();
    }
}