using Application.DTOs.ChatDTO;
using Application.Services.ChatService.ReceiverService;
using Application.Services.LoginService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MoneyManager.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MoneyManager.Controllers
{
    [Authorize]
    public class AccountController : BaseApiController
    {
        private readonly IUserService userService;
        private readonly IReceiverService receiverService;

        public AccountController(IUserService userService, IReceiverService receiverService)
        {
            this.userService = userService;
            this.receiverService = receiverService;
        }
 
        [HttpGet]
        public async Task<IActionResult> GetReciverUsers()
        {

            try
            {
                _ = int.TryParse(User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value, out int id);
                var role = User.FindFirst(x => x.Type == ClaimsIdentity.DefaultRoleClaimType).Value;

                IEnumerable<MessegeReceiversSendersDTO> result = new List<MessegeReceiversSendersDTO>();

                if (role == "Doctor")
                {
                    var pacients = await receiverService.GetPacientsForDoctor(await userService.GetDoctorIdByUserId(id));
                    var admins = await receiverService.GetAdminsForDoctor(await userService.GetDoctorIdByUserId(id));
                    result = pacients.Union(admins);
                }
                else if (role == "Admin")
                {
                    result = await receiverService.GetDoctorsForAdmin(await userService.GetAdminIdByUserId(id));

                }
                else if (role == "User")
                {
                    result = await receiverService.GetDoctorsForPacient(await userService.GetPacientIdByUserId(id));
                }

                result = receiverService.SetOnlineStatus(result, ChatHub.Users);

                return Ok(result);
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
