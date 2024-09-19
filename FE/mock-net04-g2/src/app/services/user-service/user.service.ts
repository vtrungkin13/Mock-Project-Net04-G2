import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { User } from '../../models/User';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  baseUrl = 'http://localhost:5221';

  constructor(private http: HttpClient) {}

  updateUser(userId: number, newUser: any): Observable<any> {
    return this.http
      .put<any>(`${this.baseUrl}/api/user/update-user/${userId}`, newUser)
      .pipe(
        catchError((error) => {
          return throwError(() => error);
        })
      );
  }
}
