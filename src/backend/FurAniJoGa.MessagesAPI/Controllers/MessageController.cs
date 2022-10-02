using FurAniJoGa.Domain;
using FurAniJoGa.Infrastructure;
using MessagesAPI.Dto;
using MessagesAPI.MessageQueue.Events;
using Microsoft.AspNetCore.Mvc;

namespace MessagesAPI.Controllers;


[ApiController]
[Route("api")]
public class MessageController : Controller
{
    private readonly IMessageRepository _messageRepository;
    private readonly ILogger<MessageController> _logger;

    public MessageController(IMessageRepository messageRepository, ILogger<MessageController> logger)
    {
        _messageRepository = messageRepository;
        _logger = logger;
    }
    
    [HttpGet("messages")]
    public async Task<ActionResult<ReadMessageDto>> GetMessages(int page, int size, bool fromEnd, CancellationToken token)
    {
        _logger.LogInformation("Page: {Page}, Size: {Size}, From end: {FromEnd}", page, size ,fromEnd);
        return Ok( ( await _messageRepository.GetMessages(page, size, fromEnd, token) )
                  .Select(ReadMessageDto.FromMessage) );
    }
}