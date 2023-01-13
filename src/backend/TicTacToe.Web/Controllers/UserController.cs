using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Web.Managers;
using TicTacToe.Web.Models;
using TicTacToe.Web.ViewModels;

namespace TicTacToe.Web.Controllers;

[ApiController]
public class UserController : Controller
{
    private readonly UserManger _userManager;
    
    public UserController(UserManger userManager)
    {
        _userManager = userManager;
    }
    
    
    /// <summary>
    /// Register user
    /// </summary>
    /// <response code="400">Something went wrong</response>
    /// <response code="204">User registered</response>
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromForm] RegisterUserDTO dto)
    {
        var (username, password) = ( dto.Name, dto.Password );
        var user = new User()
        {
            UserName = username,
            NormalizedUserName = username.ToUpper(),
        };
        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded)
        {
            return BadRequest(new
            {
                Message = "Error while registration",
                Errors = result.Errors.Select(err => err.Description)
            });
        }

        var readUserDto = new ReadUserDTO(user.UserName);
        return Ok(readUserDto);
    }
    
    [HttpGet("/users")]
    public IActionResult GetUsersPaged([FromQuery(Name = "q")] string? query,
        [Required] [FromQuery(Name = "page")] [Range(1, int.MaxValue)]
        int pageNumber,
        [Required] [FromQuery(Name = "size")] [Range(1, int.MaxValue)]
        int pageSize)
    {
        var users = _userManager.GetUsersPaged(pageNumber, pageSize);
        return Ok(users);
    }
}