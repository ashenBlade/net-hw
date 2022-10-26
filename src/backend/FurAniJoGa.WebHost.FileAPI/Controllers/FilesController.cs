using FurAniJoGa.FileAPI.Abstractions;
using FurAniJoGa.RabbitMq.Contracts.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace FurAniJoGa.WebHost.FileAPI.Controllers;

[ApiController]
[Route("/api/files")]
public class FilesController: ControllerBase
{
    private readonly IFileService _fileService;
    private readonly ILogger<FilesController> _logger;
    private readonly IBus _bus;
    private readonly IFileMetadataRepository _fileMetadataRepository;

    public FilesController(IFileService fileService, IBus bus,IFileMetadataRepository fileMetadataRepository, ILogger<FilesController> logger)
    {
        _fileService = fileService;
        _bus = bus;
        _fileMetadataRepository = fileMetadataRepository;
        _logger = logger;
    }
    
    [HttpGet("{fileId:guid}")]
    public async Task<IActionResult> GetFileInfo(Guid fileId, CancellationToken token = default)
    {
        var file = await _fileService.GetFileInfoAsync(fileId, token);
        if (file is null)
        {
            _logger.LogDebug("File with id: {FileId} not found", fileId);
            return NotFound();
        }

        var metadata = _fileMetadataRepository.GetMetadataByIdAsync(fileId, token);
        
        return Ok(new {file.Filename, file.ContentType, file.FileId, metadata});
    }

    [HttpGet("{id:guid}/blob")]
    public async Task<IActionResult> GetFileContent(Guid id, CancellationToken token = default)
    {
        var fileContent = await _fileService.DownloadFileAsync(id, token);
        if (fileContent is null)
        {
            _logger.LogDebug("File with id: {FileId} not found", id);
            return NotFound();
        }
        
        return File(fileContent.Content, 
                    fileContent.ContentType,
                    fileContent.Filename);
    }

    [HttpPost("")]
    public async Task<IActionResult> UploadFile(IFormFile file, CancellationToken token = default)
    {
        _logger.LogInformation("Attempt to upload file");
        Guid fileId;
        try
        {
            await using var stream = file.OpenReadStream();
            fileId = await _fileService.SaveFileAsync(stream, file.FileName, file.ContentType, token);
            
            await _bus.Publish(new FileUploadedEvent() {FileId = fileId}, token);
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "Could not save file. Error during call SaveFileAsync");
            return Problem("Could not save file to storage");
        }
        
        return Ok(new{FileId = fileId});
    }
}