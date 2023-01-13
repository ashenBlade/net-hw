using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Web.GameRepository;
using TicTacToe.Web.Managers;
using TicTacToe.Web.ViewModels;

namespace TicTacToe.Web.Controllers;

[ApiController]
[Route("/api/games")]
public class GamesController : ControllerBase
{
    private readonly IGameRepository _gameManager;

    public GamesController(UserManger userManager, IGameRepository gameManager)
    {
        _gameManager = gameManager;
    }
    
    [HttpGet("")]
    public IActionResult GetGames([Required] 
                                  [FromQuery(Name = "page")] [Range(1, int.MaxValue)]
                                  int pageNumber,
                                  [Required] 
                                  [FromQuery(Name = "size")] [Range(1, int.MaxValue)]
                                  int pageSize)
    {
        var games = _gameManager.GetGamesPagedAsync(pageNumber, pageSize);
        return Ok(games);
    }
}
