using Application.Services.DoctorService;
using Application.Services.LoginService;
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
        private IDoctorService doctorService;
        private IUserService userService;

        public DoctorProfileController(IDoctorService doctorService, IUserService userService)
        {
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
    }
}
