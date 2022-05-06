using Application.Services.DoctorService;
using Application.Services.LoginService;
using Application.Services.PacientService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MoneyManager.Controllers
{

    public class AccountController : BaseApiController
    {

        private readonly IUserService userService;
        private readonly IDoctorService doctorService;
        private readonly IPacientsService pacientsService;

        public AccountController(IDoctorService doctorService, IUserService userService, IPacientsService pacientsService)
        {
            this.userService = userService;
            this.doctorService = doctorService;
            this.pacientsService = pacientsService;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetReciverUsers()
        {

            try
            {
                _ = int.TryParse(User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value, out int id);
                var role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;

                if (role == "Doctor")
                {
                    return Ok(await pacientsService.GetDoctorPacients(await userService.GetDoctorIdByUserId(id)));

                }
                if (role == "Admin")
                {
                    return Ok(await doctorService.GetAdminDoctors(await userService.GetAdminIdByUserId(id)));

                }
                if (role == "User")
                {
                    return Ok(await doctorService.GetPacientDoctors(await userService.GetPacientIdByUserId(id)));

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
