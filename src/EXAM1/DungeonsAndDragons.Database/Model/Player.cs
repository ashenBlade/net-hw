using System.ComponentModel.DataAnnotations;

namespace DungeonsAndDragons.Database.Model;

public class Player
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }
    
    [Required]
    public int ClassId { get; set; }
    [Required]
    public Class Class { get; set; }
    
    [Required]
    public int RaceId { get; set; }
    [Required]
    public Race Race { get; set; }
    
    
}