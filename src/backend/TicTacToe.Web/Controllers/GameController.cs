using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Web.Managers;
using TicTacToe.Web.ViewModels;

namespace TicTacToe.Web.Controllers;

[ApiController]
public class GameController : Controller
{
    private readonly GameManager _gameManager;

    public GameController(UserManger userManager, GameManager gameManager)
    {
        _gameManager = gameManager;
    }
    
    [HttpGet("/games")]
    public IActionResult GetGames([FromQuery(Name = "q")] string? query,
        [Required] [FromQuery(Name = "page")] [Range(1, int.MaxValue)]
        int pageNumber,
        [Required] [FromQuery(Name = "size")] [Range(1, int.MaxValue)]
        int pageSize)
    {
        var games = _gameManager.GetGamesPaged(pageNumber, pageSize);
        return Ok(games);
    }

    [HttpPost("/create")]
    public IActionResult CreateGame([FromForm] GameCreationViewModel gameOptions)
    {
        var game = _gameManager.CreateGame(gameOptions.Owner, gameOptions.MaxRank);
        return Ok(game);
    }

    public IActionResult StartGame(int id)
    {
        _gameManager.StartGame(id);
        return Ok();
    }
    
}
