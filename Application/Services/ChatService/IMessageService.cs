﻿
using Application.DTOs.ChatDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.ChatService
{
    public interface IMessageService 
    {
        void Add(MessageDTO message);
        Task<MessageDTO> DeleteMessage(MessageDeleteModelDTO messageDeleteModel, int id);
        IEnumerable<MessageDTO> GetAll();

        IEnumerable<MessageDTO> GetReceivedMessages(int userId);
    }
}
