using Application.DTOs.ChatDTO;
using Application.DTOs.UserDTOs;
using Application.Enums;
using AutoMapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.ChatService
{
    public class MessageService : BaseService, IMessageService
    {
        public MessageService(CovidHelperContext context, IMapper mapper) : base(context, mapper)
        {
           
        }

        public void Add(MessageDTO message)
        {
            var messageDB = new Message
            {
                SenderId = message.Sender,
                ReceiverId = message.Receiver,
                Content = message.Content,
                IsReceiverDeleted = message.IsReceiverDeleted,
                IsSenderDeleted = message.IsSenderDeleted,
                MessageDate = message.MessageDate,
                IsNew = true
            };
            this.context.Messages.Add(messageDB);
            context.SaveChanges();          
        }
        
        public async Task<MessageDTO> DeleteMessage(MessageDeleteModelDTO messageDeleteModel,int id)
        {
            var message = mapper.Map<MessageDTO>(await context.Messages.Where(x => x.Id == id).FirstOrDefaultAsync());
            if (messageDeleteModel.DeleteType == DeleteTypeEnum.DeleteForEveryone.ToString())
            {
                message.IsReceiverDeleted = true;
                message.IsSenderDeleted = true;
            }
            else
            {
               message.IsReceiverDeleted = message.IsReceiverDeleted || (message.Receiver == messageDeleteModel.DeletedUserId);
                message.IsSenderDeleted = message.IsSenderDeleted || (message.Sender == messageDeleteModel.DeletedUserId);
            }
            context.Update(message);
            await this.context.SaveChangesAsync();
            return message;
        }

        public async Task<IEnumerable<MessageDTO>> GetAll()
        {
            try
            {
                var messages = await context.Messages
                    .Select(x => new MessageDTO
                    {
                        Id = x.Id,
                        MessageDate = x.MessageDate,
                        Sender = x.SenderId,
                        Receiver = x.ReceiverId,
                        Content = x.Content,
                        IsSenderDeleted = x.IsSenderDeleted,
                        IsReceiverDeleted = x.IsReceiverDeleted,
                    })
                    .OrderBy(x => x.MessageDate)
                    .ToListAsync();
                return messages;              
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async Task<IEnumerable<MessageDTO>> GetReceivedMessages(int userId)
        {
            try
            {
                var messages = await context.Messages.Where(x => x.Receiver.UserId == userId).ToListAsync();
                return mapper.Map<List<Message>, List<MessageDTO>>(messages);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
