import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegisterResponse } from '../../models/RegisterReponse';
import { catchError, Observable, throwError } from 'rxjs';
import { User } from '../../models/User';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  baseUrl = "http://localhost:5221/api/Authentication";
  
  constructor(private http: HttpClient) { }

  register(registerData: any) : Observable<RegisterResponse> {
    return this.http.post<RegisterResponse>(`${this.baseUrl}/register`, registerData).pipe(
      catchError((error) => {
        return throwError(() => error);
      })
    );
  }

}
