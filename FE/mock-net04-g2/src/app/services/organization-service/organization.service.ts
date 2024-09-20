import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class OrganizationService {
  organizationUrl = 'http://localhost:5221/api/Organization';
  constructor(private http: HttpClient) {}

  getOrganizations(): Observable<any> {
    return this.http.get<any>(`${this.organizationUrl}/GetAll`);
  }
}
