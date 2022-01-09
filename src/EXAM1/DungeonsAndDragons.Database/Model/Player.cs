using System.ComponentModel.DataAnnotations;

namespace DungeonsAndDragons.Database.Model;

public class Player
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }
    
    public int ClassId { get; set; }
    public Class Class { get; set; }
    
    public int RaceId { get; set; }
    public Race Race { get; set; }
    
    
}