using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Web.GameRepository;
using TicTacToe.Web.Managers;
using TicTacToe.Web.ViewModels;

namespace TicTacToe.Web.Controllers;

[ApiController]
[Route("/api/games")]
[Authorize]
public class GamesController : ControllerBase
{
    private readonly TicTacUserManger _userManager;
    private readonly IGameRepository _gameManager;
    private readonly ILogger<GamesController> _logger;

    public GamesController(TicTacUserManger userManager, IGameRepository gameManager, ILogger<GamesController> logger)
    {
        _userManager = userManager;
        _gameManager = gameManager;
        _logger = logger;
    }
    
    [HttpGet("")]
    [AllowAnonymous]
    public async Task<IActionResult> GetGamesAsync([Required] 
                                                   [FromQuery(Name = "page")] [Range(1, int.MaxValue)]
                                                   int page,
                                                   [Required] 
                                                   [FromQuery(Name = "size")] [Range(1, int.MaxValue)]
                                                   int size)
    {
        var games = await _gameManager.GetGamesPagedAsync(page, size);
        return Ok(games);
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateGame([FromBody] CreateGameDto dto)
    {
        try
        {
            var ownerId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var game = await _gameManager.CreateGameAsync(ownerId, dto.Rank);
            return Ok(game);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Не удалось создать игру");
            return BadRequest(new {Description = "Ошибка во время создания игры"});
        }
    }
}