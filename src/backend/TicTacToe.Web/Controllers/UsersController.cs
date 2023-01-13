using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Web.JwtService;
using TicTacToe.Web.Managers;
using TicTacToe.Web.Models;
using TicTacToe.Web.ViewModels;

namespace TicTacToe.Web.Controllers;

[ApiController]
[Route("/api/users")]
public class UsersController : Controller
{
    private readonly TicTacUserManger _userManager;
    private readonly IJwtService _jwtService;
    private readonly SignInManager<User> _signInManager;


    public UsersController(TicTacUserManger userManager, IJwtService jwtService, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _jwtService = jwtService;
        _signInManager = signInManager;
    }
    
    
    /// <summary>
    /// Register user
    /// </summary>
    /// <response code="400">Something went wrong</response>
    /// <response code="204">User registered</response>
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDTO dto)
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
        
        return Ok(new
                  {
                      UserName = username,
                      AccessToken = _jwtService.CreateJwt(user),
                  });
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUserAsync([FromBody] LoginDto dto)
    {
        var (username, password) = ( dto.Username, dto.Password );
        var user = await _userManager.FindByNameAsync(username);
        if (user is null)
        {
            return NotFound();
        }

        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (signInResult.Succeeded)
        {
            return Ok(new {user.UserName, AccessToken = _jwtService.CreateJwt(user)});
        }

        return Unauthorized();
    }
    
    [HttpGet("")]
    public IActionResult GetUsersPaged([Required] [FromQuery(Name = "page")] [Range(1, int.MaxValue)]
                                       int pageNumber,
                                       [Required] [FromQuery(Name = "size")] [Range(1, int.MaxValue)]
                                       int pageSize)
    {
        var users = _userManager.GetUsersPaged(pageNumber, pageSize);
        return Ok(users);
    }
}