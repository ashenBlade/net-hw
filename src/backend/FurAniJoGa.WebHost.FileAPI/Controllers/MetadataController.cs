using FurAniJoGa.RabbitMq.Contracts.Events;
using FurAniJoGa.Worker.MongoUpdater.RedisMetadataUploaderService;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace FurAniJoGa.WebHost.FileAPI.Controllers;

[ApiController]
[Route("/api/metadata")]
public class MetadataController : Controller
{
    private readonly IMetadataUploaderService _metadataUploaderService; 
    private readonly IBus _bus;

    public MetadataController(IMetadataUploaderService metadataUploaderService, IBus bus)
    {
        _metadataUploaderService = metadataUploaderService;
        _bus = bus;
    }

    [HttpPost("")]
    public async Task<IActionResult> UploadMetadata(Guid requestId, JsonContent metadata, CancellationToken token = default)
    {
        try
        {
            await _metadataUploaderService.UploadMetadata(requestId, metadata);
        }
        catch
        {
            return BadRequest();
        }

        await _bus.Publish(new MetadataUploadedEvent() {Metadata = metadata, RequestId = requestId}, token);

        return Ok();
    }
}