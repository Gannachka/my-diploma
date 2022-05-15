namespace Application.Services.ChatService.ReceiverService
{
    using Application.DTOs.ChatDTO;
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Persistence;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ReceiverService : BaseService, IReceiverService
    {
        public ReceiverService(CovidHelperContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public async Task<List<MessegeReceiversSendersDTO>> GetAdminsForDoctor(int doctorId)
        {
            try
            {
                var adminId = (await context.Doctors
                    .FirstOrDefaultAsync(x => x.DoctorId == doctorId)).AdminId;

                var user = await context.Users
                    .Include(x => x.Admin)
                    .Where(x => x.RoleId == 2 && x.AdminId.HasValue && x.AdminId == adminId)
                    .Select(x => new MessegeReceiversSendersDTO
                    {
                        Id = x.UserId,
                        FullName = "Admin",
                        IsOnline = false,
                        IsActive = true
                    })
                    .ToListAsync();

                return user;
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<MessegeReceiversSendersDTO>> GetDoctorsForAdmin(int adminId)
        {
            try
            {
                var users = await context.Users
                    .Include(x => x.Doctor)
                    .Where(x => x.RoleId == 3 && x.DoctorId.HasValue && x.Doctor.AdminId == adminId)
                    .Select(x => new MessegeReceiversSendersDTO
                    {
                        Id = x.UserId,
                        FullName = x.Doctor.FullName,
                        IsOnline = false,
                        IsActive = true
                    })
                    .ToListAsync();

                return users;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<MessegeReceiversSendersDTO>> GetDoctorsForPacient(int pacientId)
        {
            try
            {
                var doctorId = (await context.Pacients
                    .FirstOrDefaultAsync(x => x.PatientId == pacientId)).DoctorId;

                var user = await context.Users
                    .Include(x => x.Doctor)
                    .Where(x => x.RoleId == 3 && x.DoctorId.HasValue && x.DoctorId == doctorId)
                    .Select(x => new MessegeReceiversSendersDTO
                    {
                        Id = x.UserId,
                        FullName = x.Doctor.FullName,
                        IsOnline = false,
                        IsActive = true
                    })
                    .ToListAsync();

                return user;
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<MessegeReceiversSendersDTO>> GetPacientsForDoctor(int doctorId)
        {
            try
            {
                var user = await context.Users
                    .Include(x => x.Pacient)
                    .Where(x => x.RoleId == 1 && x.PacientId.HasValue && x.Pacient.DoctorId == doctorId)
                    .Select(x => new MessegeReceiversSendersDTO
                    {
                        Id = x.UserId,
                        FullName = x.Pacient.FullName,
                        IsOnline = false,
                        IsActive = true
                    })
                    .ToListAsync();

                return user;
            }

            catch (Exception ex)
            {
                throw;
            }
        }

        public IEnumerable<MessegeReceiversSendersDTO> SetOnlineStatus(IEnumerable<MessegeReceiversSendersDTO> receiversSendersList, IList<UserConnection> connections)
        {
            try
            {
                foreach(var receiver in receiversSendersList)
                {
                    receiver.IsOnline = connections.FirstOrDefault(x => x.UserId == receiver.Id) != null;
                    receiver.IsActive = connections.FirstOrDefault(x => x.UserId == receiver.Id) != null;
                }

                return receiversSendersList;
            }

            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
