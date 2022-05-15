using Application.DTOs.ChatDTO;
using Application.Services.ChatService;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoneyManager.Hubs
{
    public class ChatHub: Hub
    {
        private readonly IMessageService messageService;
        public ChatHub(IMessageService messageService)
        {
            this.messageService = messageService;
        }

        public static IList<UserConnection> Users = new List<UserConnection>();

        public Task SendMessageToUser(MessageDTO message)
        {
            message.MessageDate = DateTime.Now;
            var reciever = Users.FirstOrDefault(x => x.UserId == message.Receiver);
            var connectionId = reciever == null ? "offlineUser" : reciever.ConnectionId;
            this.messageService.Add(message);
            return Clients.Client(connectionId).SendAsync("ReceiveDM", Context.ConnectionId, message);
        }

        //public async Task DeleteMessage(MessageDeleteModelDTO message)
        //{
        //    var deletedMessage = await this.messageService.DeleteMessage(message);
        //    await Clients.All.SendAsync("BroadCastDeleteMessage", Context.ConnectionId, deletedMessage);
        //}

        public async Task PublishUserOnConnect(int id, string fullname)
        {
            var existingUser = Users.FirstOrDefault(x => x.UserId == id);
            var indexExistingUser = Users.IndexOf(existingUser);

            UserConnection user = new()
            {
                UserId = id,
                ConnectionId = Context.ConnectionId,
                FullName = fullname
            };

            if (!Users.Contains(existingUser))
            {
                Users.Add(user);
            }
            else
            {
                Users[indexExistingUser] = user;
            }

            await Clients.All.SendAsync("BroadcastUserOnConnect", Users);

        }

        public void RemoveOnlineUser(int userID)
        {
            var user = Users.Where(x => x.UserId == userID).ToList();
            foreach (UserConnection i in user)
            {
                Users.Remove(i);
            }

            Clients.All.SendAsync("BroadcastUserOnDisconnect", Users);
        }
    }

}
