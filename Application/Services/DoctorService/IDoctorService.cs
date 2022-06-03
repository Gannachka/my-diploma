using Application.DTOs.ChatDTO;
using Application.DTOs.UserDTOs;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.DoctorService
{
    public interface IDoctorService
    {
        Task<List<DoctorDTO>> GetDoctors(int id);
        Task<List<Doctor>> GetDoctorProfile(int id);
        Task<List<DoctorDTO>> GetDoctorsNew(int doctortId);

        Task RegisterDoctor(DoctorRegistrationModelDTO registrationModel, string verificationURL, int adminId);        
    }
}
