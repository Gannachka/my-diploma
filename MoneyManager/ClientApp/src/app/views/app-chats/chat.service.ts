import { Injectable, OnInit } from '@angular/core';
import * as signalR from '@microsoft/signalr';          // import signalR
import { HttpClient } from '@angular/common/http';
import { Observable, Subject } from 'rxjs';
import { MessageDto } from './MessageDTO';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ChatService {
  readonly BaseURI = environment.apiURL;
  constructor(private http: HttpClient) {

  }
  getUserReceivedMessages() {
    return this.http.get('/api/message');
  }
  deleteMessage(message) {
    return this.http.post('/api/message', message);
  }
  getAll() {
    return this.http.get('/api/account');
  }

}
