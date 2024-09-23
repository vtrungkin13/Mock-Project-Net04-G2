import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, catchError, throwError } from 'rxjs';
import { User } from '../../models/User';
import { AuthService } from '../auth-service/auth.service';
import { ApiResponse } from '../../models/ApiResponse';
import { RoleEnum } from '../../models/enum/RoleEnum';

@Injectable({
  providedIn: 'root',
})
export class UserService {
  baseUrl = 'http://localhost:5221';

  constructor(private http: HttpClient, private authService: AuthService) {}

  updateUser(userId: number, newUser: any): Observable<any> {
    const token = this.authService.getAuthToken();
    const headers = {
      Authorization: 'Bearer ' + token,
    };
    return this.http
      .put<any>(`${this.baseUrl}/api/user/update-user/${userId}`, newUser, {
        headers: headers,
      })
      .pipe(
        catchError((error) => {
          return throwError(() => error);
        })
      );
  }

  getAllUsers(): Observable<any> {
    const token = this.authService.getAuthToken();
    const headers = {
      Authorization: 'Bearer ' + token,
    };
    return this.http
      .get<any>(`${this.baseUrl}/api/User`, {
        headers: headers,
      })
      .pipe(
        catchError((error) => {
          return throwError(() => error);
        })
      );
  }

  findUser(pageSize: number, page: number, name: string): Observable<any> {
    if (!name || name.length === 0) {
      name = '%20';
    }
    const token = this.authService.getAuthToken();
    const headers = {
      Authorization: 'Bearer ' + token,
    };
    return this.http
      .get<any>(`${this.baseUrl}/api/User/Filter/${pageSize}/${page}/${name}`, {
        headers: headers,
      })
      .pipe(
        catchError((error) => {
          return throwError(() => error);
        })
      );
  }

  filterUserCount(name: string): Observable<any> {
    if (!name || name.length === 0) {
      name = '%20';
    }
    const token = this.authService.getAuthToken();
    const headers = {
      Authorization: 'Bearer ' + token,
    };
    return this.http
      .get<any>(`${this.baseUrl}/api/User/FilterCount/${name}`, {
        headers: headers,
      })
      .pipe(
        catchError((error) => {
          return throwError(() => error);
        })
      );
  }

  changeUserRole(
    userId: number,
    newRole: RoleEnum
  ): Observable<ApiResponse<string, string>> {
    const token = this.authService.getAuthToken();
    const headers = new HttpHeaders({
      Authorization: 'Bearer ' + token,
    });

    const url = `${this.baseUrl}/api/User/Role/${userId}`;

    return this.http.put<ApiResponse<string, string>>(
      url,
      newRole,
      { headers: headers } // Options
    );
  }
}
