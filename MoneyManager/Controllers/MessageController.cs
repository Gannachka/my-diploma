namespace MoneyManager.Controllers
{
    using Application.Services.ChatService;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    [Authorize]
    public class MessageController : BaseApiController
    {
        private readonly IMessageService messageService;

        public MessageController(IMessageService messageService)
        {
           this.messageService = messageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var messages = await this.messageService.GetAll();
                return Ok(messages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Error occurred while search for transactions"
                });
            }
        }

        //[HttpPost()]
        //public async Task<IActionResult> DeleteMessage([FromBody] MessageDeleteModelDTO messageDeleteModel)
        //{
        //    var message = await this.messageService.DeleteMessage(messageDeleteModel);
        //    return Ok(message);
        //}
    }
}
