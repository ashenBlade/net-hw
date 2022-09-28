using FurAniJoGa.Domain;
using FurAniJoGa.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace MessagesAPI.Controllers;


[ApiController]
[Route("api")]
public class MessageController : Controller
{
    private readonly IMessageFactory _messageFactory;
    
    public MessageController(IMessageFactory messageFactory)
    {
        _messageFactory = messageFactory;
    }
    
    [HttpGet("messages")]
    public async Task<List<Message>> GetMessages(int page, int size,bool fromEnd)
    {
        return await _messageFactory.GetMessages(page, size, fromEnd);
    }
}