import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, of } from 'rxjs';

@Injectable()
export class CrudService {
  items: any[];
  constructor(
    private http: HttpClient
  ) {
  }

  getItems(): Observable<any> {
    return this.http.get('/api/pacients'); 
  }

  getDoctors(): Observable<any> {
    return this.http.get('/api/doctor');
  }

  getApp(): Observable<any> {
    return this.http.get('/api/appointment');
  }

  getQuestionaries(): Observable<any> {
    return this.http.get('/api/questionary');
  }

  addQuestioary(item): Observable<any> {
    return this.http.post('/api/questionary', item);
  }

  addAppointment(item): Observable<any> {
    return this.http.post('/api/appointment', item);
  }

  addPacient(item): Observable<any> {
    return this.http.post('/api/register', item);
  }

  addDoctor(item): Observable<any> {
    return this.http.post('/api/registerDoctor', item);
  }

  updateItem(id, item): Observable<any> {
    item.id = id;
    return this.http.put('/api/transaction', item);
  }

  removeItem(row): Observable<any> {
    return this.http.delete('/api/pacient?id=' + row.id);
  }
}
