import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { ResetPasswordResponse } from '../../models/ResetPasswordResponse';
import { AuthService } from '../auth-service/auth.service';

@Injectable({
  providedIn: 'root'
})
export class ResetpasswordService {
  baseUrl = "http://localhost:5221/api/Authentication/reset";

  constructor(private http: HttpClient) { }

  resetPassword(email: string) : Observable<ResetPasswordResponse> {  
    return this.http.put<ResetPasswordResponse>(`${this.baseUrl}/${email}`,{}).pipe(
      catchError((error) => {
        return throwError(() => error);
      })
    );
  }
}
