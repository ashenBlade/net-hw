using Microsoft.EntityFrameworkCore;
using TicTacToe.Web.Models;

namespace TicTacToe.Web.GameRepository;

public class DatabaseGameRepository: IGameRepository
{
    private readonly TicTacToeDbContext _context;

    public DatabaseGameRepository(TicTacToeDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<Game>> GetGamesPagedAsync(int page, int size, CancellationToken token = default)
    {
        return await _context.Games
                             .Skip(( page - 1 ) * size)
                             .Take(size)
                             .ToListAsync(token);
    }

    public async Task<Game> CreateGameAsync(int ownerId, int rank)
    {
        var insert = new Game()
                   {
                       Name = "", OwnerId = ownerId, MaxRank = rank, Status = GameStatus.Created,
                   };
        var entity = await _context.Games.AddAsync(insert);
        await _context.SaveChangesAsync();
        return entity.Entity;
    }
}