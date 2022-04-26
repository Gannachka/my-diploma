namespace Application.Services.LoginService
{
    using DTOs.UserDTOs;
    using AutoMapper;
    using Domain;
    using Infrastructure.DefaultSettings;
    using Microsoft.EntityFrameworkCore;
    using Persistence;
    using System;
    using System.Threading.Tasks;

    public class UserService : BaseService, IUserService 
    {
        public UserService(CovidHelperContext context, IMapper mapper) : base(context, mapper)
        {

        }

        public async Task<LoginDTO> GetUserByEmail(string email, string password)
        {
            try
            {
                var user = mapper.Map<LoginDTO>(await context.Users
                    .Include(x => x.Role)
                    .SingleOrDefaultAsync(x => x.Email == email && x.Password == password));

                if (user != null && user.Role != "Admin")
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
                    .SingleOrDefaultAsync(x => x.UserId == id));

                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task RegisterUser(UserRegistrationModelDTO registrationModel, string verificationURL)
        {
            try
            {
                var user = mapper.Map<User>(registrationModel);

                // TODO: Uncommit after move to doctor registration
                //var pacient = mapper.Map<Pacient>(registrationModel);
                //user.Pacient = pacient;
                await context.Users.AddAsync(user);

                await context.SaveChangesAsync();

                await EmailService.SendEmailAsync(registrationModel.Email, "Регистрация", $"Вы были зарегистрированы в системе мониторинга хронических заболеваний. </br>Для окончания регистрации перейдите по ссылке: <a href=\"{verificationURL}\">{verificationURL}</a>");
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
                var pacient = await context.Pacients.FirstOrDefaultAsync(x => x.PatientId == id);
                var user = (await context.Pacients
                    .Include(x => x.User)
                    .FirstOrDefaultAsync(x => x.PatientId == id)).User;

                if (user != null)
                {
                    pacient.Age = registrationModel.Age;
                    pacient.FullName = registrationModel.FirstName + " " + registrationModel.LastName;
                    user.Email = registrationModel.Email;

                    context.Users.Update(user);
                    context.Pacients.Update(pacient);

                }

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
