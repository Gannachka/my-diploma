using Application.DTOs.ChatDTO;
using Application.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.PacientService
{
    public interface  IPacientsService
    {
        Task <List<PacientDTO>> GetPacients(int id);

        Task DeletePacient(int id);

        Task RegisterPacient(UserRegistrationModelDTO registrationModel, string verificationURL, int doctorId);

        Task<List<MessegeRecipientsSendersDTO>> GetDoctorPacients(int id);
    }
}
