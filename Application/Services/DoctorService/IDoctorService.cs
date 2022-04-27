using Application.DTOs.UserDTOs;
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

        Task RegisterDoctor(DoctorRegistrationModelDTO registrationModel, string verificationURL, int adminId);

    }
}
