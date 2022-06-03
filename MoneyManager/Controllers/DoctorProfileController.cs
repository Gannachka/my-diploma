using Application.Services.DoctorService;
using Application.Services.LoginService;
using Application.Services.PacientService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyManager.Controllers
{
    [Authorize]
    public class DoctorProfileController : BaseApiController
    {
        private readonly IPacientsService pacientsService;
        private IDoctorService doctorService;
        private IUserService userService;

        public DoctorProfileController(IDoctorService doctorService, IUserService userService, IPacientsService pacientsService)
        {
            this.pacientsService = pacientsService;
            this.doctorService = doctorService;
            this.userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> GetPacientsAppointments(int userId)
        {
            try
            {
                if (userId > 0)
                {
                    return Ok(await doctorService.GetDoctorProfile(await userService.GetDoctorIdByUserId(userId)));

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
        public async Task<IActionResult> ChangeUserActive(int id)
        {
            try
            {
                var doctorId = await userService.GetUserIDByDoctorId(id);
                await userService.ChangeUserActive(doctorId);
                return Ok(await doctorService.GetDoctorsNew(id));
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    Message = "Error occurred while search for users"
                });
            }
        }
    }
}
