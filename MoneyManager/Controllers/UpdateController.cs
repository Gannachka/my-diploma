using Application.DTOs.UserDTOs;
using Application.Services.LoginService;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MoneyManager.Controllers
{
    public class UpdateController : BaseApiController
    {
        private readonly IUserService userService;
        public UpdateController(IUserService userService)
        {
            this.userService = userService;
        }


        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserRegistrationModelDTO registrationModel)
        {

            try
            {
                _ = int.TryParse(User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value, out int id);
                if (id > 0)
                {
                    await userService.UpdateUser(id,registrationModel);
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
