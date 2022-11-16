using FurAniJoGa.RabbitMq.Contracts.Events;
using FurAniJoGa.WebHost.FileAPI.RedisMetadataUploaderService;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace FurAniJoGa.WebHost.FileAPI.Controllers;

[ApiController]
[Route("/api/metadata")]
public class MetadataController : Controller
{
    private readonly IUploaderService _uploaderService; 
    private readonly IBus _bus;

    public MetadataController(IUploaderService uploaderService, IBus bus)
    {
        _uploaderService = uploaderService;
        _bus = bus;
    }

    [HttpPost("")]
    public async Task<IActionResult> UploadMetadata(Guid requestId, Dictionary<string,string> metadata, CancellationToken token = default)
    {
        try
        {
            await _uploaderService.UploadMetadata(requestId, metadata);
        }
        catch
        {
            return Problem("Could not save metadata to cache");
        }

        await _bus.Publish(new MetadataUploadedEvent() {Metadata = metadata, RequestId = requestId}, token);

        return Ok();
    }
}