using System.ComponentModel.DataAnnotations;

namespace DungeonsAndDragons.Database.Model;

public class Race
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    [Required]
    [Range(1, 100)]
    public int Speed { get; set; }
    [Required]
    [Range(1, 400)]
    public double Height { get; set; }
}