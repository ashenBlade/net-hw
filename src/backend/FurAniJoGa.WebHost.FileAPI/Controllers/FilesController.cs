using Microsoft.AspNetCore.Mvc;

namespace FurAniJoGa.WebHost.FileAPI.Controllers;

[ApiController]
[Route("/api/files")]
public class FilesController: ControllerBase
{
    private readonly IFileService _fileService;
    private readonly ILogger<FilesController> _logger;

    public FilesController(IFileService fileService, ILogger<FilesController> logger)
    {
        _fileService = fileService;
        _logger = logger;
    }
    
    [HttpGet("{fileId:guid}")]
    public async Task<IActionResult> GetFile(Guid fileId, CancellationToken token = default)
    {
        var file = await _fileService.GetFileAsync(fileId, token);
        if (file is null)
        {
            _logger.LogDebug("File with id: {FileId} not found", fileId);
            return NotFound();
        }

        return Ok(new {file.Filename, file.ContentType, file.FileId});
    }

    [HttpGet("{id:guid}/blob")]
    public async Task<IActionResult> GetFileContent(Guid id, CancellationToken token = default)
    {
        var stream = await _fileService.DownloadFileAsync(id, token);
        if (stream is null)
        {
            _logger.LogDebug("File with id: {FileId} not found", id);
            return NotFound();
        }

        return File(stream.Content, stream.ContentType);
    }

    [HttpPost("")]
    public async Task<IActionResult> UploadFile(IFormFile formFile, CancellationToken token = default)
    {
        try
        {
            var id = await _fileService.SaveFileAsync(formFile, token);
            return Ok(id);
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "Could not save file. Error during call SaveFileAsync");
            return Problem("Could not save file to storage");
        }
    }
}