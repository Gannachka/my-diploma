import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class InvoiceService {

  items: any[];
  constructor(
    private http: HttpClient
  ) {
  }

  getItems(): Observable<any> {
    return this.http.get('/api/pacients');
  }

  addPacient(item): Observable<any> {
    return this.http.post('/api/register', item);
  }

  addAppointment(item): Observable<any> {
    return this.http.post('/api/appointment', item);
  }
  updateItem(id, item): Observable<any> {
    item.id = id;
    return this.http.put('/api/transaction', item);
  }
}
