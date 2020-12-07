using Microsoft.AspNetCore.Mvc;
using SingletonChatApi.Models;
using SingletonChatApi.Services;

namespace SingletonChatApi.Controllers
{
    [ApiController]
    [Route("api/Message")]
    public class MessageController : ControllerBase
    {
        private readonly MessageService _messageService;

        public MessageController(MessageService service)
        {
            _messageService = service;
        }

        [HttpGet]
        public IActionResult Get() => Ok(_messageService.GetMessages());

        [HttpPost]
        public IActionResult Post([FromBody] Message message)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid message.");

            _messageService.InsertMessage(message);
            return Ok("Message sent.");
        }
    }
}