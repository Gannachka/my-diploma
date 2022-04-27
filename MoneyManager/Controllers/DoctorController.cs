namespace MoneyManager.Controllers
{
    using Application.DTOs.UserDTOs;
    using Application.Services.DoctorService;
    using Application.Services.LoginService;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    [Authorize]
    public class DoctorController : BaseApiController
    {

        private readonly IUserService userService;
        private readonly IDoctorService doctorService;

        public DoctorController(IDoctorService doctorService, IUserService userService)
        {
            this.userService = userService;
            this.doctorService = doctorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDoctors()
        {

            try
            {
                _ = int.TryParse(User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value, out int id);

                if (id > 0)
                {
                    return Ok(await doctorService.GetDoctors(await userService.GetAdminIdByUserId(id)));

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
