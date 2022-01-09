using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DungeonsAndDragons.Game.Entity.Equipments.Armor;

namespace DungeonsAndDragons.Database.Model;

public class Armor
{
    [Key]
    public int Id { get; set; }

    [Required]
    public ArmorType ArmorType { get; set; }

    [Required]
    public int ArmorClassBase { get; set; }

    [Required]
    public string Name { get; set; }
}