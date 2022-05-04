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
            this.context.Add(message);
            this.context.SaveChanges();
        }
        
        async Task<MessageDTO> IMessageService.DeleteMessage(MessageDeleteModelDTO messageDeleteModel,int id)
        {
            
          //  var message = await this.context.Messages.Where(x => x.Id == id).FirstOrDefaultAsync();
            var message = mapper.Map<MessageDTO>(await this.context.Messages.Where(x => x.Id == id).FirstOrDefaultAsync()                );
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

        IEnumerable<MessageDTO> IMessageService.GetAll()
        {
            try
            {
                var messages = this.context.Messages.OrderBy(x => x.MessageDate).ToList();
                return mapper.Map<List<Message>, List<MessageDTO>>(messages);
              
            }
            catch (Exception)
            {

                throw;
            }

        }
        IEnumerable<MessageDTO> IMessageService.GetReceivedMessages(int userId)
        {
            try
            {
                var messages = this.context.Messages.Where(x => x.Receiver.UserId == userId).ToList();
                return mapper.Map<List<Message>, List<MessageDTO>>(messages);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
