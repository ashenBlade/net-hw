using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Web.ViewModels;

public class RegisterUserDTO
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Password { get; set; }
}