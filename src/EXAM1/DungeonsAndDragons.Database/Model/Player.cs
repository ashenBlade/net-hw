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
    [Required]
    public int WeaponId { get; set; }
    [Required]
    public Weapon Weapon { get; set; }
    [Required]
    public int ArmorId { get; set; }
    [Required]
    public Armor Armor { get; set; }
}