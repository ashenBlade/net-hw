using System.ComponentModel.DataAnnotations;
using DungeonsAndDragons.Shared.Models;

namespace DungeonsAndDragons.Shared;

public class FightStartDTO
{
    [Required]
    public Entity Player { get; set; }
    [Required]
    public Entity Monster { get; set; }
}