using Application.DTOs.UserDTOs;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.DoctorService
{
    public class DoctorService : BaseService, IDoctorService
    {
        public DoctorService(CovidHelperContext context, IMapper mapper) : base(context, mapper)
        {

        }
        public async Task<List<DoctorDTO>> GetDoctors(int id)
        {
            try
            {
                var user = await context.Users
                    .Include(x => x.Doctor)
                    .Where(x => x.RoleId == 3 && x.DoctorId.HasValue && x.Doctor.AdminId == id)
                    .ToListAsync();

                return mapper.Map<List<User>, List<DoctorDTO>>(user);
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task RegisterDoctor(DoctorRegistrationModelDTO registrationModel, string verificationURL, int adminId)
        {
            try
            {
                var user = mapper.Map<User>(registrationModel);

                var doctor = mapper.Map<Doctor>(registrationModel);
                doctor.AdminId = adminId;
                user.Doctor = doctor;

                await context.Users.AddAsync(user);

                await context.SaveChangesAsync();

                await EmailService.SendEmailAsync(registrationModel.Email, "Регистрация", $"Вы были зарегистрированы в системе мониторинга хронических заболеваний. </br>Для окончания регистрации перейдите по ссылке: <a href=\"{verificationURL}\">{verificationURL}</a>");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
