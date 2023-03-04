using GrpcChat.Domain;
using GrpcChat.TokenGenerator;
using GrpcChat.Web.Controllers.Authentication.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GrpcChat.Web.Controllers.Authentication;

[AllowAnonymous]
[Route("/api")]
public class AuthenticationController: ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly ILogger<AuthenticationController> _logger;

    public AuthenticationController(UserManager<User> userManager, 
                                    ITokenGenerator tokenGenerator,
                                    ILogger<AuthenticationController> logger)
    {
        _userManager = userManager;
        _tokenGenerator = tokenGenerator;
        _logger = logger;
    }
    
    [HttpPost("token")]
    public async Task<IActionResult> CreateUser([FromBody]CreateUserDto dto, CancellationToken token)
    {
        var user = new User() {Email = dto.Email, UserName = dto.Name,};
        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
        {
            _logger.LogError("Ошибка во время создания пользователя. Ошибки: {Errors}. Почта: {Email}. Имя: {Name}", result.Errors.Select(x => x.Description), dto.Email, dto.Name);
            return BadRequest(new {Errors = result.Errors.Select(x => x.Description)});
        }
        _logger.LogInformation("Создан пользователь с именем {UserName}, почта: {Email}", user.UserName, user.Email);

        return Ok(new {AccessToken = _tokenGenerator.GenerateToken(user.Email)});
    }
}