using System.ComponentModel.DataAnnotations;

namespace GrpcChat.Auth.Web.Controllers.Authentication.Dto;

public class CreateTokenDto
{
    [Required]
    public string UserName { get; set; } = null!;
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}