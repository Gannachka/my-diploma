namespace MoneyManager.Controllers
{
    using Application.DTOs.UserDTOs;
    using Application.Services.LoginService;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    public class SetupCompleteController : BaseApiController
    {
        private readonly IUserService userService;

        public SetupCompleteController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CompleteSetup(PasswordSetupModelDTO registrationModel)
        {
            try
            {
                await userService.CompleteSetup(registrationModel);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
