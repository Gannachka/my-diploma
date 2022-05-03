import { Injectable, OnInit } from '@angular/core';
import * as signalR from '@microsoft/signalr';          // import signalR
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { MessageDto } from './MessageDTO';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SignalrClientService {

  userName: string;
  connection: signalR.HubConnection;
  messenger = new Subject<ReceiveMessage>();
  onlineUsers = new Subject<User[]>();

  constructor() {
  }

  sendMessage(message: string) {
    return this.connection.invoke("SendMessage", this.userName, message)
  }
  openConnection() {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(`/api/chatsocket?username=${this.userName}`)
      .build();

    this.connection.start().then(res => {
      console.log("connected");
    });
    // add handler
    this.chatMessageHandler();
  }
  chatMessageHandler() {
    this.connection.on("ReceiveMessage", (user, message) => {
      this.messenger.next({
        userName: user, message: message, isSender: false
      });
    });
    this.connection.on("OnlineUsers", (user: User[]) => {
      //console.log("user", user);
      this.onlineUsers.next(user);
    });
  }
  onSendMessage() {
    this._signalrClientService.sendMessage(this.message).then(res => {
      this.chatContainer.push({ message: this.message, userName: this._signalrClientService.userName, isSender: true })
    });
  }
  openUserDialog() {
    const dialogRef = this.dialog.open(UserDetailsComponent, { hasBackdrop: false });
    dialogRef.afterClosed().subscribe((userName: string) => {
      this.connectHub(userName);
    });
  }
  connectHub(userName: string) {
    this._signalrClientService.userName = userName;
    this._signalrClientService.openConnection();
  }
}
