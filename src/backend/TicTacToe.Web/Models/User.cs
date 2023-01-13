using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace TicTacToe.Web.Models;

public class User : IdentityUser<int>
{
    [Required]
    public int Rank { get; set; }
}