using Application.DTOs.QuestionarityDTO;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.AppointmentService
{
    public interface IAppointmentService
    {
        Task<List<Appointment>> GetAppointments(int id);

        Task CreateAppointment(AppointmentsDTO appointment);
    }
}
