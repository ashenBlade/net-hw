using System.ComponentModel.DataAnnotations;

namespace DungeonsAndDragons.Database.Model;

public class Monster
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    
    [Required]
    public int RaceId { get; set; }
    public Race Race{ get; set; }
    
    [Required]
    public int ClassId { get; set; }
    public Class Class { get; set; }

    [Required]
    public Characteristics Characteristics { get; set; }
}