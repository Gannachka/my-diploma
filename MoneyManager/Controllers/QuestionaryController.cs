namespace MoneyManager.Controllers
{
    using Application.DTOs.QuestionarityDTO;
    using Application.Services.LoginService;
    using Application.Services.QuestionaryService;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    [Authorize]
    public class QuestionaryController:BaseApiController
    {
        private readonly IQuestionaryService questionaryService;
        private readonly IUserService userService;

        public QuestionaryController(IQuestionaryService questionaryService, IUserService userService)
        {
            this.questionaryService = questionaryService;
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyQuestionaries()
        {
            try
            {
                _ = int.TryParse(User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value, out int id);

                if (id > 0)
                {
                    return Ok(await questionaryService.GetQuestionaires(await userService.GetPacientIdByUserId(id)));
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

        [HttpPost]
        public async Task<IActionResult> CreateQuestionaries(QuestionarityDTO questinary)
        {
            try
            {
                _ = int.TryParse(User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value, out int id);

                if (id > 0)
                {
                    return Ok(await questionaryService.CreateQuestionairy(await userService.GetPacientIdByUserId(id), questinary));
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
