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
                             .Include(g => g.Owner)
                             .Include(g => g.SecondPlayer)
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

    public async Task<Game?> FindActiveGameByIdAsync(int gameId)
    {
        return await _context.Games
                             .Include(g => g.Owner)
                             .Include(g => g.SecondPlayer)
                             .SingleOrDefaultAsync(g => g.Id == gameId && g.Status == GameStatus.Created);
    }

    public async Task<Game> UpdateGameAsync(Game game)
    {
        _context.Games.Update(game);
        await _context.SaveChangesAsync();
        return game;
    }

    public async Task<Game?> FindGameByUserIdAsync(int userId)
    {
        return await _context.Games
                             .Where(g => ( g.OwnerId == userId || g.SecondPlayerId == userId ) && 
                                         ( g.Status == GameStatus.Playing || g.Status == GameStatus.Created ))
                             .Include(g => g.Owner)
                             .Include(g => g.SecondPlayer)
                             .FirstOrDefaultAsync();
    }
}