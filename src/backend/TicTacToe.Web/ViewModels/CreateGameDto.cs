using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Web.ViewModels;

public class CreateGameDto
{
    [Required]
    [Range(1, int.MaxValue)]
    public int Rank { get; set; }
}