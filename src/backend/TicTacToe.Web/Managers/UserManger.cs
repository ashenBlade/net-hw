using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using TicTacToe.Web.Models;

namespace TicTacToe.Web.Managers;

public class UserManger : UserManager<User>
{
    private readonly ILogger<UserManager<User>> _logger;
    private readonly TicTacToeDbContext _context;

    public UserManger(IUserStore<User> store, IOptions<IdentityOptions> optionsAccessor, 
        IPasswordHasher<User> passwordHasher, IEnumerable<IUserValidator<User>> userValidators, 
        IEnumerable<IPasswordValidator<User>> passwordValidators, ILookupNormalizer keyNormalizer, 
        IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<User>> logger, TicTacToeDbContext context) 
        : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, 
            keyNormalizer, errors, services, logger)
    {
        _logger = logger;
        _context = context;
    }
    
    

    public Task<List<User>> GetUsersPaged(int pageNumber, int pageSize)
    {
        return _context.Users
            .OrderBy(u => u.Id)
            .Skip(( pageNumber - 1 ) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }
}