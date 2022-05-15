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
    public class PacientsController : BaseApiController
    {
        private readonly IPacientsService pacientsService;
        private readonly IUserService userService;

        public PacientsController(IPacientsService pacientsService, IUserService userService)
        {
            this.pacientsService = pacientsService;
            this.userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> GetPasients()
        {

            try
            {
                _ = int.TryParse(User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value, out int id);

                if (id > 0)
                {
                    return Ok(await pacientsService.GetPacients(await userService.GetDoctorIdByUserId(id)));

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

        [HttpDelete]
        public async Task<IActionResult> DeletePacient(int id)
        {
            try
            {
                _ = int.TryParse(User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value,
                    out int userId);

                if (userId > 0)
                {
                    await pacientsService.DeletePacient(id);
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
                    Message = "Error occurred while create transaction"
                });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser(UserRegistrationModelDTO registrationModel)
        {

            try
            {
                _ = int.TryParse(User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value, out int id);
                if (id > 0)
                {
                    await userService.UpdateUser(id, registrationModel);
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
