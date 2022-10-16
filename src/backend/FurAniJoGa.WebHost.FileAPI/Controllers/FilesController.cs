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
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetFile(Guid id, CancellationToken token = default)
    {
        var file = await _fileService.DownloadFileAsync(id, token);
        if (file is null)
        {
            return NotFound();
        }

        return File(file.Stream, file.ContentType, file.Filename);
    }

    [HttpPost("")]
    public async Task<IActionResult> UploadFile(IFormFile formFile, CancellationToken token = default)
    {
        // var file = new File()
        //            {
        //                Filename = formFile.Name, 
        //                Stream = formFile.OpenReadStream(),
        //                ContentType = formFile.ContentType
        //            };
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