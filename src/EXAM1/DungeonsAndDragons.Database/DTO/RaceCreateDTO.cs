using System.ComponentModel.DataAnnotations;

namespace DungeonsAndDragons.Database.DTO;

public class RaceCreateDTO
{
    [Required]
    public string Name { get; set; }
    [Required]
    [Range(1, 100)]
    public int Speed { get; set; }
    [Required]
    public double Height { get; set; }
}