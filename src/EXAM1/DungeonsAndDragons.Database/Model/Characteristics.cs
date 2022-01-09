using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;

namespace DungeonsAndDragons.Database.Model;

[Owned]
public class Characteristics
{
    [Required]
    [Range(1, 30)]
    public int Strength { get; set; }
    
    [Required]
    [Range(1, 30)]
    public int Dexterity { get; set; }
    
    [Required]
    [Range(1, 30)]
    public int Constitution { get; set; }
    
    [Required]
    [Range(1, 30)]
    public int Intelligence { get; set; }
    
    [Required]
    [Range(1, 30)]
    public int Wisdom { get; set; }
    
    [Required]
    [Range(1, 30)]
    public int Charisma { get; set; }
}