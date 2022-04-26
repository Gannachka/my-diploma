namespace Application.Services.LoginService
{
    using DTOs.UserDTOs;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Persistence;
    using System;
    using System.Threading.Tasks;
    using System.Linq;
    using Domain;

    public class UserService : BaseService, IUserService 
    {
        public UserService(CovidHelperContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public async Task CompleteSetup(PasswordSetupModelDTO passwordSetupModel)
        {
            try
            {
                var user = mapper.Map<User>(passwordSetupModel);

                var existingUser = await context.Users
                    .Where(x => x.Email == user.Email)
                    .FirstOrDefaultAsync();

                if (existingUser != null)
                {
                    existingUser.Password = user.Password;
                    context.Users.Update(existingUser);
                    await context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Пользователь не найден");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> GetDoctorIdByUserId(int userId)
        {
            try
            {
                var doctorId = await context.Users
                    .Where(x => x.UserId == userId)
                    .Select(x => x.DoctorId)
                    .FirstOrDefaultAsync();

                return doctorId.GetValueOrDefault();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<LoginDTO> GetUserByEmail(string email, string password)
        {
            try
            {
                var user = mapper.Map<LoginDTO>(await context.Users
                    .Include(x => x.Role)
                    .Include(x => x.Doctor)
                    .Include(x => x.Pacient)
                    .SingleOrDefaultAsync(x => x.Email == email && x.Password == password));

                if (user != null && user.Role != "Admin" && user.Role != "Doctor")
                {
                    await EmailService.SendEmailAsync(email, "НАПОМИНАНИЕ", "ДЕД, ПРИМИ ТАБЛЕТКИ!");
                }

                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<LoginDTO> GetUserById(int id)
        {
            try
            {
                var user = mapper.Map<LoginDTO>(await context.Users
                    .Include(x => x.Role)
                    .Include(x => x.Doctor)
                    .Include(x => x.Pacient)
                    .SingleOrDefaultAsync(x => x.UserId == id));

                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateUser(int id, UserRegistrationModelDTO registrationModel)
        {
            try
            {
                var pacient = await context.Pacients
                    .Include(x => x.User)
                    .FirstOrDefaultAsync(x => x.User.UserId == id);

                if (pacient != null)
                {
                    pacient.Age = registrationModel.Age;
                    pacient.FullName = registrationModel.FullName;
                    pacient.User.Email = registrationModel.Email;

                    context.Pacients.Update(pacient);
                }

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateDoctor(int id, DoctorRegistrationModelDTO registrationModel)
        {
            try
            {
                var doctor = await context.Doctors
                    .Include(x => x.User)
                    .FirstOrDefaultAsync(x => x.User.UserId == id);

                if (doctor != null)
                {
                    doctor.WorkExperience = registrationModel.WorkExperience;
                    doctor.FullName = registrationModel.FullName;
                    doctor.PhoneNumber = registrationModel.PhoneNumber;
                    doctor.User.Email = registrationModel.Email;

                    context.Doctors.Update(doctor);
                }

                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
