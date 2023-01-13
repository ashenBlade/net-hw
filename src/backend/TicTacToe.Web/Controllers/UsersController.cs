using System.ComponentModel.DataAnnotations;
using System.Xml.XPath;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicTacToe.Web.JwtService;
using TicTacToe.Web.Managers;
using TicTacToe.Web.Models;
using TicTacToe.Web.ViewModels;

namespace TicTacToe.Web.Controllers;

[ApiController]
public class UsersController : Controller
{
    private readonly TicTacUserManger _ticTacUserManager;
    private readonly IJwtService _jwtService;
    private readonly SignInManager<User> _signInManager;


    public UsersController(TicTacUserManger ticTacUserManager, IJwtService jwtService, SignInManager<User> signInManager)
    {
        _ticTacUserManager = ticTacUserManager;
        _jwtService = jwtService;
        _signInManager = signInManager;
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
        var result = await _ticTacUserManager.CreateAsync(user, password);
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
    public async Task<IActionResult> LoginUserAsync(LoginDto dto)
    {
        var (username, password) = ( dto.Username, dto.Password );
        var user = await _ticTacUserManager.FindByNameAsync(username);
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
    
    [HttpGet("/users")]
    public IActionResult GetUsersPaged([Required] [FromQuery(Name = "page")] [Range(1, int.MaxValue)]
                                       int pageNumber,
                                       [Required] [FromQuery(Name = "size")] [Range(1, int.MaxValue)]
                                       int pageSize)
    {
        var users = _ticTacUserManager.GetUsersPaged(pageNumber, pageSize);
        return Ok(users);
    }
}