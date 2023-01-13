using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Web.Models;

namespace TicTacToe.Web;

public class TicTacToeDbContext: IdentityDbContext<User, IdentityRole<int>, int>
{
    // public DbSet<User> Users => Set<User>();
    public DbSet<Game> Games => Set<Game>();

    public TicTacToeDbContext(DbContextOptions<TicTacToeDbContext> options)
        : base(options)
    { }
}