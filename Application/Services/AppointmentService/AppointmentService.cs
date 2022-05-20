using Application.DTOs.QuestionarityDTO;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AppointmentService
{
    public class AppointmentService : BaseService, IAppointmentService
    {
        public AppointmentService(CovidHelperContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task CreateAppointment(AppointmentsDTO appointment, int pacienId)
        {
            var appointmentDB = mapper.Map<Appointment>(appointment);
            appointmentDB.PacientId = pacienId;
            context.Appointments.Add(appointmentDB);
            await context.SaveChangesAsync();
        }
        //public async Task<List<Appointment>> GetPacientsAppointments(int id)
        //{
        //    return await context.Appointments
        //       .Include(x => x.PacientId)
        //       .Where(x => x.Pacient.DoctorId == id)
        //       .Select(x => new PacientsQuestionarityDTO
        //       {
                 
        //       })
        //       .ToListAsync();

        //}
        public async Task<List<Appointment>> GetAppointments(int id)
        {
            return await context.Appointments.Where(x => x.PacientId == id).ToListAsync();
        }
    }
}
