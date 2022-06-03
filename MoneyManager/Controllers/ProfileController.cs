using Application.Services.LoginService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MoneyManager.Controllers
{
    public class ProfileController : BaseApiController
    {
        private readonly IUserService userService;
      
        public ProfileController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserSettings()
        {
            try
            {
                _ = int.TryParse(User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value, out int id);
                var role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;

                if (role == "Doctor")
                {
                    return Ok(await userService.GetDoctorProfile(await userService.GetDoctorIdByUserId(id)));
                }
                else if (role == "User")
                {
                    return Ok(await userService.GetPacientProfile(await userService.GetPacientIdByUserId(id)));
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Error occurred"
                });
            }
        }
    }
}
