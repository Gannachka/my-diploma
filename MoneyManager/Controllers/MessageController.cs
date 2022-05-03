using Application.DTOs.ChatDTO;
using Application.Services.ChatService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MoneyManager.Controllers
{
    [Route("api/message")]
    [ApiController]
    public class MessageController : BaseApiController
    {

        private readonly IMessageService messageService;
        public MessageController(IMessageService messageService)
        {
           this.messageService = messageService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var messages = this.messageService.GetAll();
            return Ok(messages);
        }


        [HttpGet("received-messages/{userId}")]
        public IActionResult GetUserReceivedMessages(int userId)
        {
            var messages = this.messageService.GetReceivedMessages(userId);
            return Ok(messages);
        }
        //[HttpPost()]
        //public async Task<IActionResult> DeleteMessage([FromBody] MessageDeleteModelDTO messageDeleteModel)
        //{
        //    var message = await this.messageService.DeleteMessage(messageDeleteModel);
        //    return Ok(message);
        //}
    }
}
