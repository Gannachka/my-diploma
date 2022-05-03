import { Component, OnInit } from '@angular/core';
import { ChatService } from './chat.service';
import { MessageDto } from './MessageDTO';

@Component({
  selector: 'app-root',
  templateUrl: './app-chats.component.html',
  styleUrls: ['./app-chats.component.scss']
})
export class ChatComponent implements OnInit {

  message: string;
  chatContainer: ReceiveMessage[] = [];
  onlineUsers: User[];
  subscription: Subscription;

  constructor(
    public dialog: MatDialog,
    private _signalrClientService: SignalrClientService) {

    this.subscription = new Subscription();

    //subscribing chat message and online user details
    this.subscription.add(this._signalrClientService.messenger.subscribe((res: ReceiveMessage) => {
      this.chatContainer.push(res);
    }));
    this.subscription.add(this._signalrClientService.onlineUsers.subscribe((res: User[]) => {
      this.onlineUsers = res;
    }));

  }

  ngOnInit(): void {
    this.openUserDialog();
  }
  ngOnDestroy() {
    this.subscription.unsubscribe();
  }
}
