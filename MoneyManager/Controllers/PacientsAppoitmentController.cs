﻿using Application.Services.AppointmentService;
using Application.Services.LoginService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MoneyManager.Controllers
{
    [Authorize]
    public class PacientsAppoitmentController : BaseApiController
    {
        private IAppointmentService appointmentService;
        private IUserService userService;

        public PacientsAppoitmentController(IAppointmentService appointmentService, IUserService userService)
        {
            this.appointmentService = appointmentService;
            this.userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPacientsAppointments(int userId)
        {
            try
            {
                if (userId > 0)
                {
                    return Ok(await appointmentService.GetAppointments(await userService.GetPacientIdByUserId(userId)));

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
