import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { AuthService } from '../auth-service/auth.service';

@Injectable({
  providedIn: 'root'
})
export class DonateService {
  donateUrl = 'http://localhost:5221/api/Donate';

  constructor(private http: HttpClient, private authService: AuthService) { }

  GetDonationHistory(uid: number): Observable<any> {
    const token = this.authService.getAuthToken();
    const headers = {
      Authorization: 'Bearer ' + token,
    };
    return this.http
      .get<any>(`${this.donateUrl}/Donate/${uid}`,{
        headers: headers,
      })
      .pipe(
        catchError((error) => {
          return throwError(() => new Error(error.message));
        })
      );
  }
}
