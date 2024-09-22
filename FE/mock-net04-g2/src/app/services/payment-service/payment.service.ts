import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable, throwError } from 'rxjs';
import { AuthService } from '../auth-service/auth.service';

@Injectable({
  providedIn: 'root',
})
export class PaymentService {
  baseUrl = 'http://localhost:5221/api/Payment/CreatePayment';

  constructor(private http: HttpClient) {}

  createPayment(redirectUrl: string, paymentRequest: any): Observable<any> {
    return this.http
      .post<any>(`${this.baseUrl}?redirectUrl=${redirectUrl}`, paymentRequest)
      .pipe(
        catchError((error) => {
          return throwError(() => error);
        })
      );
  }
}
