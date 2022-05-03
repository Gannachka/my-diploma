using Application.DTOs.UserDTOs;
using Application.Services.DoctorService;
using Application.Services.LoginService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MoneyManager.Controllers
{
    public class RegisterDoctorController : BaseApiController
    {
        private readonly IUserService userService;
        private readonly IDoctorService doctorService;

        public RegisterDoctorController(IUserService userService, IDoctorService doctorService)
        {
            this.userService = userService;
            this.doctorService = doctorService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RegisterDoctor(DoctorRegistrationModelDTO registrationModel)
        {
            try
            {
                _ = int.TryParse(User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value,
                    out int doctorId);

                var adminId = await userService.GetAdminIdByUserId(doctorId);

                var URL = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/sessions/completesetup";
                await doctorService.RegisterDoctor(registrationModel, URL, adminId);
                return Ok(await doctorService.GetDoctors(adminId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Error occurred during registration");
            }
        }
    }
}

