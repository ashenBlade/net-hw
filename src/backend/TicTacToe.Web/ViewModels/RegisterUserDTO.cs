using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Web.ViewModels;

public class RegisterUserDTO
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}