using Application.DTOs.UserDTOs;
using Domain;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace Application.Services.LoginService
{
    public interface IUserService
    {
        Task<LoginDTO> GetUserByEmail(string email, string password);

        Task<LoginDTO> GetUserById(int id);

        Task UpdateUser(int id, UserRegistrationModelDTO registrationModel);

        Task UpdateDoctor(int id, DoctorRegistrationModelDTO registrationModel);

        Task<int> GetDoctorIdByUserId(int userId);

        Task<int> GetPacientIdByUserId(int userId);

        Task<int> GetAdminIdByUserId(int userId);

        Task CompleteSetup(PasswordSetupModelDTO passwordSetupModel);
    }
}
