using FurAniJoGa.Domain;
using FurAniJoGa.Infrastructure;
using MessagesAPI.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MessagesAPI.Controllers;


[ApiController]
[Route("api")]
public class MessageController : Controller
{
    private readonly IMessageManager _messageManager;
    
    public MessageController(IMessageManager messageManager)
    {
        _messageManager = messageManager;
    }
    
    [HttpGet("messages")]
    public async Task<ActionResult<ReadMessageDto>> GetMessages(int page, int size,bool fromEnd)
    {
        return Ok( ( await _messageManager.GetMessages(page, size, fromEnd) ).Select(ReadMessageDto.FromMessage) );
    }
}