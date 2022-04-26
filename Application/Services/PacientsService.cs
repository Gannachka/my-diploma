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

namespace Application.Services
{
    public class PacientsService : BaseService, IPacientsService
    {
        public PacientsService(CovidHelperContext context, IMapper mapper) : base(context, mapper)
        {

        }
        public async Task <List<UserDTO>>  GetPacients (int id)
        {
            try
            {
                var user = await context.Users.Where(x => x.RoleId == 1).ToListAsync();              

                return mapper.Map<List<User>,List<UserDTO>>(user);
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task  DeletePacient(int id)
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


    }
}
