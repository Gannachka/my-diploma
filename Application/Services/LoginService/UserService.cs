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
                    await EmailService.SendEmailAsync(email);
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

        public async Task RegisterUser(RegistrationModelDTO registrationModel)
        {
            try
            {
                var user = mapper.Map<User>(registrationModel);
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task UpdateUser(int id,RegistrationModelDTO registrationModel)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.UserId == id);
                if (user != null)
                {
                   user.Age = registrationModel.Age;
                   user.FullName = registrationModel.FirstName + " " + registrationModel.LastName;
                   user.Email = registrationModel.Email;

                    context.Users.Update(user);

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
