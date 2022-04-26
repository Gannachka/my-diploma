namespace MoneyManager.Controllers
{
    using Application.DTOs.UserDTOs;
    using Application.Services.LoginService;
    using Application.Services.PacientService;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    public class RegisterController : BaseApiController
    {
        private readonly IUserService userService;
        private readonly IPacientsService pacientsService;

        public RegisterController(IUserService userService, IPacientsService pacientsService)
        {
            this.userService = userService;
            this.pacientsService = pacientsService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RegisterUser(UserRegistrationModelDTO registrationModel)
        {
            try
            {
                _ = int.TryParse(User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value,
                    out int userId);

                var doctorId = await userService.GetDoctorIdByUserId(userId);

                var URL = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/sessions/completesetup";
                await pacientsService.RegisterPacient(registrationModel, URL, doctorId);
                return Ok(await pacientsService.GetPacients(doctorId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error occurred during registration");
            }
        }
    }
}
