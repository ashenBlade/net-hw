using DungeonsAndDragons.Database.Data;
using DungeonsAndDragons.Database.DTO;
using DungeonsAndDragons.Database.Model;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Mvc;

namespace DungeonsAndDragons.Database.Controllers;

[ApiController]
[Route("api/dnd/")]
[Route("api/[controller]")]
public class DungeonsAndDragonsController : ControllerBase
{
    private readonly IGameRepository _repo;
    private readonly ILogger<DungeonsAndDragonsController> _logger;

    public DungeonsAndDragonsController(IGameRepository repo,
                                        ILogger<DungeonsAndDragonsController> logger)
    {
        _repo = repo;
        _logger = logger;
    }

    [HttpGet]
    [Route("classes")]
    public async Task<IActionResult> GetAllClassesAsync()
    {
        _logger.LogInformation("Hit GetAllClassesAsync");
        var classes = new List<ClassReadDTO>();
        await foreach (var model in _repo.GetAllClassesAsync())
        {
            classes.Add(ClassReadDTO.FromModel(model));
        }
        _logger.LogTrace("Retrieved {ClassesAmount} classes from database", classes.Count);
        return Ok(classes);
    }

    [HttpGet(Name = "GetClassById")]
    [Route("classes/{id:int}")]
    public async Task<IActionResult> GetClassByIdAsync(int id)
    {
        _logger.LogInformation("Hit GetClassByIdAsync with id: {Id}", id);
        var model = await _repo.GetClassByIdAsync(id);
        if (model is null)
        {
            _logger.LogTrace("Class with id: {Id} not found", id);
            return NotFound();
        }
        var dto = ClassReadDTO.FromModel(model);
        return Ok(dto);
    }

    [HttpPost]
    [Route("classes")]
    public async Task<IActionResult> PostClassAsync(ClassCreateDTO dto)
    {
        _logger.LogInformation("Hit PostClassAsync");
        var model = ClassCreateDTO.ToModel(dto);
        var id = _repo.AddClassAsync(model);
        try
        {
            await _repo.SaveChangesAsync();
            return CreatedAtAction("GetClassById", new {Id = id}, model);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Could not save new class");
            return new StatusCodeResult(500);
        }
    }

    [HttpGet]
    [Route("monsters")]
    public async Task<IActionResult> GetAllMonstersAsync()
    {
        _logger.LogInformation("Hit GetAllMonstersAsync");
        var list = new List<MonsterReadDTO>();
        await foreach (var monster in _repo.GetAllMonstersAsync())
        {
            list.Add(MonsterReadDTO.FromModel(monster));
        }

        return Ok(list);
    }

    [HttpGet(Name = "GetMonsterById")]
    [Route("monsters/{id:int}")]
    public async Task<IActionResult> GetMonsterByIdAsync(int id)
    {
        _logger.LogInformation("Hit GetMonsterByIdAsync with id: {Id}", id);
        var monster = await _repo.GetMonsterByIdAsync(id);
        if (monster is null)
        {
            _logger.LogTrace("Monster with id: {Id} not found. Sending NotFound()", id);
            return NotFound();
        }
        
        return Ok(MonsterReadDTO.FromModel(monster));
    }
}