using Microsoft.EntityFrameworkCore;
using TicTacToe.Web.Models;

namespace TicTacToe.Web;

public class TicTacToeDbContext: DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Game> Games => Set<Game>();

    public TicTacToeDbContext(DbContextOptions<TicTacToeDbContext> options)
        : base(options)
    {       
    }
}