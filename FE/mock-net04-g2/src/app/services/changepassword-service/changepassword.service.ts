import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { ChangePasswordResponse } from '../../models/ChangePasswordReponse';
import { AuthService } from '../auth-service/auth.service';

@Injectable({
  providedIn: 'root'
})
export class ChangepasswordService {
  baseUrl = "http://localhost:5221/api/User/Password";

  constructor(private http: HttpClient, private authService: AuthService) {
   }

  changePassword(changePasswordData: any) : Observable<ChangePasswordResponse> {
    const token = this.authService.getAuthToken();
    const headers = {
      Authorization: 'Bearer ' + token,
    };
    const user = this.authService.getUser();
    return this.http.put<ChangePasswordResponse>(`${this.baseUrl}/${user.id}`, changePasswordData,{
      headers: headers,
    }).pipe(
      catchError((error) => {
        return throwError(() => error);
      })
    );
  }
}
