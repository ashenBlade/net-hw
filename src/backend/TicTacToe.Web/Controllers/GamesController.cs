using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Web.GameRepository;
using TicTacToe.Web.Managers;

namespace TicTacToe.Web.Controllers;

[ApiController]
[Route("/api/games")]
[Authorize]
public class GamesController : ControllerBase
{
    private readonly TicTacUserManger _userManager;
    private readonly IGameRepository _gameManager;

    public GamesController(TicTacUserManger userManager, IGameRepository gameManager)
    {
        _userManager = userManager;
        _gameManager = gameManager;
    }
    
    [HttpGet("")]
    [AllowAnonymous]
    public async Task<IActionResult> GetGamesAsync([Required] 
                                                   [FromQuery(Name = "page")] [Range(1, int.MaxValue)]
                                                   int pageNumber,
                                                   [Required] 
                                                   [FromQuery(Name = "size")] [Range(1, int.MaxValue)]
                                                   int pageSize)
    {
        var games = await _gameManager.GetGamesPagedAsync(pageNumber, pageSize);
        return Ok(games);
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateGame([Required][Range(1, int.MaxValue)]
                                                int rank)
    {
        var ownerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var game = await _gameManager.CreateGameAsync(ownerId, rank);
        return Ok(game);
    }
}