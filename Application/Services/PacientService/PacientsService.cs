using Application.DTOs.ChatDTO;
using Application.DTOs.UserDTOs;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.PacientService
{
    public class PacientsService : BaseService, IPacientsService
    {
        public PacientsService(CovidHelperContext context, IMapper mapper) : base(context, mapper)
        {

        }
        public async Task <List<PacientDTO>> GetPacients(int id)
        {
            try
            {
                var user = await context.Users
                    .Include(x => x.Pacient)
                    .Where(x => x.RoleId == 1 && x.PacientId.HasValue && x.Pacient.DoctorId == id)
                    .ToListAsync();        

                return mapper.Map<List<User>,List<PacientDTO>>(user);
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<PacientDTO>> GetPacientsNew(int pacientId)
        {
            try
            {
                var pacient = await context.Pacients.FirstOrDefaultAsync(x => x.PatientId == pacientId);
                var user = await context.Users
                    .Include(x => x.Pacient)
                    .Where(x => x.RoleId == 1 && x.PacientId.HasValue && x.Pacient.DoctorId == pacient.DoctorId)
                    .ToListAsync();

                return mapper.Map<List<User>, List<PacientDTO>>(user);
            }

            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task DeletePacient(int id)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.UserId == id);
                if (user != null)
                {
                    context.Users.Remove(user);
                }

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task RegisterPacient(UserRegistrationModelDTO registrationModel, string verificationURL, int doctorId)
        {
            try
            {
                var user = mapper.Map<User>(registrationModel);

                var pacient = mapper.Map<Pacient>(registrationModel);
                pacient.DoctorId = doctorId;
                user.Pacient = pacient;

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
