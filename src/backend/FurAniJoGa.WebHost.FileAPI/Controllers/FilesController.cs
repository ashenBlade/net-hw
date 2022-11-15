using FurAniJoGa.RabbitMq.Contracts.Events;
using FurAniJoGa.WebHost.FileAPI.RedisMetadataUploaderService;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace FurAniJoGa.WebHost.FileAPI.Controllers;

[ApiController]
[Route("/api/files")]
public class FilesController: ControllerBase
{
    private readonly IFileService _fileService;
    private readonly ILogger<FilesController> _logger;
    private readonly IUploaderService _uploaderService;
    private readonly IBus _bus;

    public FilesController(IFileService fileService, ILogger<FilesController> logger, IUploaderService uploaderService, IBus bus)
    {
        _fileService = fileService;
        _logger = logger;
        _uploaderService = uploaderService;
        _bus = bus;
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

        return Ok(new {file.Filename, file.ContentType, file.FileId});
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
    public async Task<IActionResult> UploadFile(Guid requestId, IFormFile file, CancellationToken token = default)
    {
        _logger.LogInformation("Attempt to upload file");
        Guid fileId;
        try
        {
            fileId = await _fileService.SaveFileAsync(file, token);
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "Could not save file. Error during call SaveFileAsync");
            return Problem("Could not save file to storage");
        }

        try
        {
            await _uploaderService.UploadFileId(requestId, fileId);
        }
        catch (Exception e)
        {
            _logger.LogWarning(e, "Could not save file id in redis. Error during call UploadFileId");
            return Problem("Could not save file id to cache");
        }
        await _bus.Publish(new FileUploadedEvent() {FileId = fileId, RequestId = requestId}, token);

        return Ok(new{FileId = fileId});
    }
}