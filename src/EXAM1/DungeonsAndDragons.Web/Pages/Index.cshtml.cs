using System.Reflection.Metadata;
using DungeonsAndDragons.Shared;
using DungeonsAndDragons.Shared.Models;
using DungeonsAndDragons.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DungeonsAndDragons.Web.Pages;

public class IndexModel : PageModel
{
    private readonly ILogger<IndexModel> _logger;
    private readonly IRandomMonsterRetriever _retriever;
    private readonly IFightService _fightService;

    [BindProperty]
    public Entity Player { get; set; } = new Entity()
                                         {
                                             Name = "Some user",
                                             Weapon = 1,
                                             ArmorClass = 13,
                                             AttackModifier = 3,
                                             DamageCount = 1,
                                             DamageMax = 8,
                                             HitPoints = 50,
                                             DamageModifier = 4,
                                             AttackPerRound = 1
                                         };
    
    public FightEndDTO? FightResults { get; set; }
    
    public Entity? Enemy { get; set; }
    public IndexModel(ILogger<IndexModel> logger, 
                      IRandomMonsterRetriever retriever,
                      IFightService fightService)
    {
        _logger = logger;
        _retriever = retriever;
        _fightService = fightService;
    }

    public void OnGet()
    {
        
    }
    
    public async Task OnPostAsync()
    {
        _logger.LogInformation("Hit OnPost");
        if (!ModelState.IsValid)
        {
            _logger.LogInformation("Invalid model");
            return;
        }

        if (Player.DamageCount < 0 || Player.DamageMax < 0 || Player.DamageModifier < 0 || Player.AttackModifier < 0 || Player.AttackPerRound < 0 || Player.Weapon < 0 || Player.HitPoints < 0 || Player.ArmorClass < 0 || Player.DamageMax < 0)
        {
            ModelState.AddModelError("", "Player stats must be positive");
            return;
        }
        
        var monster = await _retriever.GetRandomMonsterAsync();
        if (monster is null)
        {
            ModelState.AddModelError("", "Could not find monster to fight");
            return;
        }
        var fightStartDto = new FightStartDTO {Player = Player, Monster = monster,};
        FightResults = await _fightService.SimulateFightAsync(fightStartDto);
        Enemy = monster;
        Player = fightStartDto.Player;
    }
}
