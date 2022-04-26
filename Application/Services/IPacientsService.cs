using Application.DTOs.UserDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public interface  IPacientsService
    {
        Task <List<UserDTO>> GetPacients(int id);

        Task DeletePacient(int id);
    }
}
