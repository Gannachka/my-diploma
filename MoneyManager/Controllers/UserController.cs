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
    public class UserController : BaseApiController
    {
        private readonly IPacientsService pacientsService;
        private IDoctorService doctorService;
        private IUserService userService;

        public UserController(IDoctorService doctorService, IUserService userService, IPacientsService pacientsService)
        {
            this.pacientsService = pacientsService;
            this.doctorService = doctorService;
            this.userService = userService;
        }

        [HttpPut]
        public async Task<IActionResult> ChangeUserActive(int id)
        {
            try
            {
                var pacientId = await userService.GetPacientIdByUserId(id);
                await userService.ChangeUserActive(id);
                return Ok(await pacientsService.GetPacientsNew(pacientId));
                
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
