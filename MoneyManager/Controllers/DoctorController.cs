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

    [Authorize]
    public class DoctorController : BaseApiController
    {
        private readonly IPacientsService pacientsService;
        private readonly IUserService userService;

        public DoctorController(IPacientsService pacientsService, IUserService userService)
        {
            this.pacientsService = pacientsService;
            this.userService = userService;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(DoctorRegistrationModelDTO registrationModel)
        {

            try
            {
                _ = int.TryParse(User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value, out int id);
                if (id > 0)
                {
                    await userService.UpdateDoctor(id, registrationModel);
                    return StatusCode(200);
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
                    Message = "Authentication failed"
                });
            }

        }
    }
}
