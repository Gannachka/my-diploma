import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, of } from 'rxjs';

@Injectable()
export class ProfileService {
  constructor(
    private http: HttpClient
  ) {  }

  getDoctorProfile(): Observable<any> {
    return this.http.get('/api/profile');
  }
}
