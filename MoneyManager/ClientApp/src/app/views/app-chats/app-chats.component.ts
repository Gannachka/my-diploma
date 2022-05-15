import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { ChatService } from './chat.service';
import { LocalStoreService } from '../../shared/services/local-store.service';
import localeRu from '@angular/common/locales/ru';
import { registerLocaleData } from '@angular/common';

registerLocaleData(localeRu);

@Component({
  selector: 'app-root',
  templateUrl: './app-chats.component.html',
  styleUrls: ['./app-chats.component.css']
})
export class AppChatsComponent implements OnInit {
  searchParameter: string = '';
  loggedInUser = this.ls.getItem('EGRET_USER');
  users: any;
  allUsers: any;
  chatUser: any;
  messages: any[] = [];
  displayMessages: any[] = []
  message: string
  hubConnection: HubConnection;


  connectedUsers: any[] = []
  constructor(
    private router: Router,
    private messageService: ChatService,
    private ls: LocalStoreService  ) { }

  ngOnInit() {
    this.messageService.getUserReceivedMessages().subscribe((item: any) => {
      if (item) {
        this.messages = item;
        this.messages.forEach(x => {
          x.type = x.receiver === this.loggedInUser.id ? 'recieved' : 'sent';
        })
        console.log(this.messages);
      }
    });

    this.messageService.getAll().subscribe(
      (user: any) => {
        if (user) {
          this.users = user.filter(x => x.id !== this.loggedInUser.id); 
          this.users.forEach(item => {
            item['isActive'] = false;
          })
          this.allUsers = this.users;
          this.makeItOnline();
        }
      },
      err => {
        console.log(err);
      },
    );

    this.message = ''
    this.hubConnection = new HubConnectionBuilder().withUrl('/chatsocket').build(); 
    const self = this
    this.hubConnection.start()
      .then(() => {
        self.hubConnection.invoke("PublishUserOnConnect", this.loggedInUser.id, this.loggedInUser.displayName)
          .then(() => console.log('User Sent Successfully'))
          .catch(err => console.error(err));

        this.hubConnection.on("BroadcastUserOnConnect", Usrs => {
          this.connectedUsers = Usrs;
          this.makeItOnline();
        })
        this.hubConnection.on("BroadcastUserOnDisconnect", Usrs => {
          this.connectedUsers = Usrs;
          this.users.forEach(item => {
            item.isOnline = false;
          });
          this.makeItOnline();
        })
      })
      .catch(err => console.log(err));

    // this.hubConnection.on("UserConnected", (connectionId) => this.UserConnectionID = connectionId);

    this.hubConnection.on('BroadCastDeleteMessage', (connectionId, message) => {
      let deletedMessage = this.messages.find(x => x.id === message.id);
      if (deletedMessage) {
        deletedMessage.isReceiverDeleted = message.isReceiverDeleted;
        deletedMessage.isSenderDeleted = message.isSenderDeleted;
        if (deletedMessage.isReceiverDeleted && deletedMessage.receiver === this.chatUser.id) {
          this.displayMessages = this.messages.filter(x => (x.receiver === this.loggedInUser.id && x.sender === this.chatUser.id) || (x.receiver === this.chatUser.id && x.sender === this.loggedInUser.id));
        }
      }
    });

    this.hubConnection.on('ReceiveDM', (connectionId, message) => {
      console.log(message);
      message.type = 'recieved';
      this.messages.push(message);
      let curentUser = this.users.find(x => x.id === message.sender);
      this.chatUser = curentUser;
      this.users.forEach(item => {
        item['isActive'] = false;
      });
      var user = this.users.find(x => x.id == this.chatUser.id);
      user['isActive'] = true;
      this.displayMessages = this.messages.filter(x => (x.receiver === this.loggedInUser.id && x.sender === this.chatUser.id) || (x.receiver === this.chatUser.id && x.sender === this.loggedInUser.id));
    })
  }

  SendDirectMessage() {
    if (this.message != '' && this.message.trim() != '') {
      var msg = {
        sender: this.loggedInUser.id,
        receiver: this.chatUser.id,
        messageDate: new Date(),
        type: 'sent',
        content: this.message
      };
      this.messages.push(msg);
      this.displayMessages = this.messages.filter(x => (x.receiver === this.loggedInUser.id && x.sender === this.chatUser.id) || (x.receiver === this.chatUser.id && x.sender === this.loggedInUser.id));

      this.hubConnection.invoke('SendMessageToUser', msg)
        .then(() => console.log('Message to user Sent Successfully'))
        .catch(err => console.error(err));
      this.message = '';
    }
  }

  openChat(user) {
    this.users.forEach(item => {
      item['isActive'] = false;
    });
    user['isActive'] = true;
    this.chatUser = user;
    this.displayMessages = this.messages.filter(x => (x.receiver === this.loggedInUser.id && x.sender === this.chatUser.id) || (x.receiver === this.chatUser.id && x.sender === this.loggedInUser.id));
  }

  makeItOnline() {
    if (this.connectedUsers && this.users) {
      this.connectedUsers.forEach(item => {
        var u = this.users.find(x => x.id == item.userId);
        if (u) {
          u.isOnline = true;
        }
      })
    }
  }

  searchReceivers() {
    this.users = this.allUsers.filter(u => u.fullName.includes(this.searchParameter));
    this.makeItOnline();
  }

  deleteMessage(message, deleteType, isSender) {
    let deleteMessage = {
      'deleteType': deleteType,
      'message': message,
      'deletedUserId': this.loggedInUser.id
    }
    this.hubConnection.invoke('DeleteMessage', deleteMessage)
      .then(() => console.log('publish delete request'))
      .catch(err => console.error(err));
    message.isSenderDeleted = isSender;
    message.isReceiverDeleted = !isSender;
  }

  onLogout() {
    this.hubConnection.invoke("RemoveOnlineUser", this.loggedInUser.id)
      .then(() => {
        this.messages.push('User Disconnected Successfully')
      })
      .catch(err => console.error(err));
    localStorage.removeItem('token');
    this.router.navigate(['/user/login']);
  }

}
