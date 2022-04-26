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

        Task RegisterUser(UserRegistrationModelDTO registrationModel, string verificationURL);

        Task UpdateUser(int id, UserRegistrationModelDTO registrationModel);
    }
}
