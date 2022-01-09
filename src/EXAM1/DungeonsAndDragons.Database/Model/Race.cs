using System.ComponentModel.DataAnnotations;

namespace DungeonsAndDragons.Database.Model;

public class Race
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }
    public int Speed { get; set; }
    public double Height { get; set; }
}