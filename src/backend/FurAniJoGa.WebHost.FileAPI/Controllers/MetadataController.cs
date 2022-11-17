using FurAniJoGa.RabbitMq.Contracts.Events;
using FurAniJoGa.WebHost.FileAPI.Dto;
using FurAniJoGa.WebHost.FileAPI.RedisMetadataUploaderService;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FurAniJoGa.WebHost.FileAPI.Controllers;

[ApiController]
[Route("api/metadata")]
public class MetadataController : ControllerBase
{
    private readonly IUploaderService _uploaderService; 
    private readonly IBus _bus;
    private readonly ILogger<MetadataController> _logger;

    public MetadataController(IUploaderService uploaderService, IBus bus, ILogger<MetadataController> logger)
    {
        _uploaderService = uploaderService;
        _bus = bus;
        _logger = logger;
    }

    [HttpPost("")]
    public async Task<IActionResult> UploadMetadata(UploadMetadataDto dto, 
                                                    CancellationToken token = default)
    
    {
        var (requestId, metadata) = ( dto.RequestId, dto.Metadata );
        _logger.LogInformation("Metadata saving requested for request {RequestId}", requestId);
        try
        {
            await _uploaderService.UploadMetadata(requestId, metadata);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Error during saving metadata to cache");
            return Problem("Could not save metadata to cache");
        }
        _logger.LogInformation("Metadata was saved to cache. Publishing event");

        await _bus.Publish(new MetadataUploadedEvent() {Metadata = metadata, RequestId = requestId}, token);
        _logger.LogInformation("Event was published");
        return Ok();
    }
}