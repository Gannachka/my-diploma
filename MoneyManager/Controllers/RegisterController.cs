namespace MoneyManager.Controllers
{
    using Application.DTOs.UserDTOs;
    using Application.Services.LoginService;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    public class RegisterController : BaseApiController
    {
        private readonly IUserService userService;

        public RegisterController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(UserRegistrationModelDTO registrationModel)
        {
            try
            {
                var URL = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/verify";
                await userService.RegisterUser(registrationModel, URL);
                return Ok(new 
                { 
                    Message = "Registration finished successfully"
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error occurred during registration");
            }
        }
    }
}
