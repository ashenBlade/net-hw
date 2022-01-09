using System.ComponentModel.DataAnnotations;
using DungeonsAndDragons.Database.Model;
using Microsoft.AspNetCore.Components.Routing;

namespace DungeonsAndDragons.Database.DTO;

public class ClassCreateDTO
{
    [Required]
    public string Name { get; set; }

    [Required]
    [Range(1, 100)]
    public int MaxGameDiceValue { get; set; }
    
    [Required]
    [Range(1, 100)]
    public int GameDiceAmount { get; set; }

    public static Class ToModel(ClassCreateDTO dto)
    {
        return new Class()
               {
                   Name = dto.Name,
                   HitsGameDice = new GameDice() {DiceAmount = dto.GameDiceAmount, MaxValue = dto.MaxGameDiceValue}
               };
    }
}