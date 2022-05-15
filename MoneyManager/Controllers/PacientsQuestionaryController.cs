using Application.Services.LoginService;
using Application.Services.QuestionaryService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MoneyManager.Controllers
{
    public class PacientsQuestionaryController : BaseApiController
    {
        private readonly IQuestionaryService questionaryService;
        private readonly IUserService userService;
        public PacientsQuestionaryController(IQuestionaryService questionaryService, IUserService userService)
        {
            this.questionaryService = questionaryService;
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPacientsQuestionary()
        {         

            try
            {
                _ = int.TryParse(User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value, out int id);

                if (id > 0)
                {
                    return Ok(await questionaryService.GetPacientsQuestionaires(id));
                }

                return BadRequest(new
                {
                    Message = "User can't be found"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Error occurred while search for transactions"
                });
            }
        }

    }
}
