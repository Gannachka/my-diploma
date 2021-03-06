using Application.DTOs.QuestionarityDTO;
using Application.Services.AppointmentService;
using Application.Services.QuestionaryService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MoneyManager.Controllers
{
    [Authorize]
    public class AppointmentController:BaseApiController
    {
        private IAppointmentService appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            this.appointmentService = appointmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMyAppointments()
        {
            try
            {
                _ = int.TryParse(User.FindFirst(x => x.Type == ClaimsIdentity.DefaultNameClaimType).Value, out int id);

                if (id > 0)
                {
                    return Ok(await appointmentService.GetAppointments(id));

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

        [HttpPost]
        public async Task<IActionResult> CreateAppointment(AppointmentsDTO appointments)
        {
            try
            {
                await appointmentService.CreateAppointment(appointments);
                return StatusCode(200);
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
