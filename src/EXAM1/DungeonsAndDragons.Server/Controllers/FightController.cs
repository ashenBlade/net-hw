using DungeonsAndDragons.Server.Services;
using DungeonsAndDragons.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DungeonsAndDragons.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class FightController : ControllerBase
{
    private readonly IFightSimulator _fightSimulator;
    private readonly ILogger<FightController> _logger;

    public FightController(IFightSimulator fightSimulator, ILogger<FightController> logger)
    {
        _fightSimulator = fightSimulator;
        _logger = logger;
    }
    // POST fight/simulate
    [HttpPost]
    [Route("simulate")]
    public async Task<IActionResult> SimulateFight([FromBody]FightStartDTO dto)
    {
        _logger.LogInformation("Hit SimulateFight");
        var fightResult = await _fightSimulator.SimulateFightAsync(dto);
        _logger.LogInformation("Fight results: ");
        foreach (var log in fightResult.Logs)
        {
            _logger.LogInformation(log.PlayerStatus);
        }
        return Ok(fightResult);
    }
}