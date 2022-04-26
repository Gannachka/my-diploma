using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MoneyManager.Controllers
{
    [Authorize]
    public class PacientsController : BaseApiController
    {
        private readonly IPacientsService pacientsService;

        public PacientsController(IPacientsService pacientsService)
        {
            this.pacientsService = pacientsService;
        }


        [HttpGet]
        public async Task<IActionResult> GetPasients()
        {

            try
            {
                _ = int.TryParse(User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value, out int id);

                if (id > 0)
                {
                    return Ok(await pacientsService.GetPacients(id));

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

    }
}
