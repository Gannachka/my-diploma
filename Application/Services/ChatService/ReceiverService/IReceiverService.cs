namespace Application.Services.ChatService.ReceiverService
{
    using Application.DTOs.ChatDTO;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IReceiverService
    {
        Task<List<MessegeReceiversSendersDTO>> GetDoctorsForPacient(int pacientId);

        Task<List<MessegeReceiversSendersDTO>> GetDoctorsForAdmin(int adminId);

        Task<List<MessegeReceiversSendersDTO>> GetPacientsForDoctor(int doctorId);

        Task<List<MessegeReceiversSendersDTO>> GetAdminsForDoctor(int doctorId);

        IEnumerable<MessegeReceiversSendersDTO> SetOnlineStatus(IEnumerable<MessegeReceiversSendersDTO> receiversSendersList, IList<UserConnection> connections);
    }
}
