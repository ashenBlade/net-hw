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
    public Entity Player { get; set; }
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

    public async Task OnPost()
    {
        _logger.LogInformation("Hit OnPost");
        var monster = await _retriever.GetRandomMonsterAsync();
        var fightStartDto = new FightStartDTO() {Player = Player, Monster = monster,};
        FightResults = await _fightService.SimulateFightAsync(fightStartDto);
        Enemy = monster;
    }
}
