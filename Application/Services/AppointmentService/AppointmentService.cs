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

        public async Task CreateAppointment(AppointmentsDTO appointment)
        {
            var appointmentDB = mapper.Map<Appointment>(appointment);
            context.Appointments.Add(appointmentDB);
            await context.SaveChangesAsync();
        }
        public async Task<List<Appointment>> GetPacientsAppointments(int id)
        {
            return await context.Questionaire
               .Include(x => x.Pacient)
               .Where(x => x.Pacient.DoctorId == id)
               .Select(x => new PacientsQuestionarityDTO
               {
                   Fullname = x.Pacient.FullName,
                   Comments = x.Comments,
                   Temperature = x.Temperature,
                   QDate = x.Date,
                   Headache = x.Headache,
                   ObstructedBreathing = x.ObstructedBreathing,
                   Tiredness = x.Tiredness
               })
               .ToListAsync();
            return await context.Appointments.Where(x => x.UserId == id).ToListAsync();
        }
        public async Task<List<Appointment>> GetAppointments(int id)
        {
            return await context.Appointments.Where(x => x.UserId == id).ToListAsync();
        }
    }
}
