using DungeonsAndDragons.Server.Services;
using DungeonsAndDragons.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DungeonsAndDragons.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class FightController : ControllerBase
{
    private readonly IFightSimulator _fightSimulator;

    public FightController(IFightSimulator fightSimulator)
    {
        _fightSimulator = fightSimulator;
    }
    // POST fight/simulate
    [HttpPost]
    [Route("simulate")]
    public async Task<IActionResult> SimulateFight([FromBody]FightStartDTO dto)
    {
        var fightResult = await _fightSimulator.SimulateFightAsync(dto);
        return Ok(fightResult);
    }
}