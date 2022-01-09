using System.ComponentModel.DataAnnotations;
using DungeonsAndDragons.Database.Model;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;

namespace DungeonsAndDragons.Database.DTO;

public class PlayerCreateDTO
{
    [Required]
    public string Name { get; set; }
    [Required]
    public int ClassId { get; set; }
    [Required]
    public int ArmorId { get; set; }
    [Required]
    public int WeaponId { get; set; }
    [Required]
    public int RaceId { get; set; }

    public static Player ToModel(PlayerCreateDTO dto)
    {
        return new Player()
               {
                   Name = dto.Name,
                   ClassId = dto.ClassId,
                   RaceId = dto.RaceId,
                   ArmorId = dto.ArmorId,
                   WeaponId = dto.WeaponId,
               };
    }
}