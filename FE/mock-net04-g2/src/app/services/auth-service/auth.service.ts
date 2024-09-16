import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { User } from '../../models/User';
import { LoginResponse } from '../../models/LoginResponse';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  baseUrl = 'http://localhost:5221/api/Authentication';

  constructor(private http: HttpClient) {}

  login(loginData: any): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(`${this.baseUrl}/login`, loginData).pipe(
      catchError((error) => {
        return throwError(() => error);
      })
    );
  }

  // Retrieve token from sessionStorage
  getAuthToken(): string | null {
    return sessionStorage.getItem('token');
  }

  // Retrieve user details from sessionStorage
  getUser(): User {
    const user = sessionStorage.getItem('user');
    return user ? JSON.parse(user) : null;
  }

  // Clear sessionStorage on logout
  clearSession() {
    sessionStorage.removeItem('token');
    sessionStorage.removeItem('user');
  }
}
