using System.ComponentModel.DataAnnotations;

namespace GrpcChat.Web.Controllers.Authentication.Dto;

public class CreateUserDto
{
    [Required]
    [MinLength(6)]
    public string Name { get; set; } = null!;

    [Required]
    public string Email { get; set; } = null!;

    [Required]
    [MinLength(6)]
    public string Password { get; set; } = null!;
}