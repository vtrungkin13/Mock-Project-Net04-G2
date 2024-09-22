import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Campaign } from '../../models/Campaign';
import { catchError, Observable, throwError } from 'rxjs';
import { CampaignStatusEnum } from '../../models/enum/CampaignStatusEnum';
import { UpdateCampaignRequest } from '../../models/UpdateCampaignRequest';
import { CampaignDetailResponse } from '../../models/CampaignDetailResponse ';
import { CampaignDetailRequest } from '../../models/CampaignDetailRequest ';

@Injectable({
  providedIn: 'root',
})
export class CampaignService {
  campaignApiUrl = 'http://localhost:5221/api/Campaigns';

  constructor(private http: HttpClient) {}

  filteredCampaigns(status: CampaignStatusEnum, page: number): Observable<any> {
    return this.http
      .get<any>(`${this.campaignApiUrl}/Status/${status}/${page}`)
      .pipe(
        catchError((error) => {
          return throwError(() => new Error(error.message));
        })
      );
  }

  totalFilteredCampaigns(status: CampaignStatusEnum): Observable<any> {
    return this.http
      .get<any>(
        `${this.campaignApiUrl}/TotalCampaignsCountAfterFilter/${status}`
      )
      .pipe(
        catchError((error) => {
          return throwError(() => new Error(error.message));
        })
      );
  }

  getCampaigns(
    pageSize: number,
    page: number,
    code: string,
    phone: string,
    status?: CampaignStatusEnum
  ) {
    // Create an object for query parameters
    const params: { [key: string]: any } = {
      pageSize,
      page,
      ...(code.trim() ? { code } : {}),
      ...(phone.trim() ? { phone } : {}),
      ...(status !== undefined ? { status } : {}),
    };

    // Convert the params object to a query string
    const queryString = new URLSearchParams(params as any).toString();

    // Make the HTTP GET request with the route segment "Filter" and query parameters
    return this.http
      .get<any>(`${this.campaignApiUrl}/Filter?${queryString}`)
      .pipe(
        catchError((error) => {
          return throwError(() => new Error(error.message));
        })
      );
  }

  getCampaignsCount(
    code: string,
    phone: string,
    status?: CampaignStatusEnum
  ): Observable<any> {
    // Create an object for query parameters
    const params: { [key: string]: any } = {
      ...(code.trim() ? { code } : {}),
      ...(phone.trim() ? { phone } : {}),
      ...(status !== undefined ? { status } : {}),
    };

    // Convert the params object to a query string
    const queryString = new URLSearchParams(params as any).toString();

    // Make the HTTP GET request with the route segment "FilterCount" and query parameters
    return this.http
      .get<any>(`${this.campaignApiUrl}/FilterCount?${queryString}`)
      .pipe(
        catchError((error) => {
          return throwError(() => new Error(error.message));
        })
      );
  }

  getCampaignDetail(id: number): Observable<any> {
    return this.http.get<any>(`${this.campaignApiUrl}/Detail/${id}`).pipe(
      catchError((error) => {
        return throwError(() => new Error(error.message));
      })
    );
  }

  addCampaign(request: CampaignDetailRequest): Observable<any> {
    const token = sessionStorage.getItem('token'); // Retrieve token from sessionStorage

    // Set the Authorization header with the token
    const headers = {
      Authorization: `Bearer ${token}`, // Include Bearer token in headers
    };

    return this.http.post(`${this.campaignApiUrl}/Add-Campaign`, request, {
      headers: headers,
    });
  }

  extendCampaign(
    id: number,
    request: UpdateCampaignRequest
  ): Observable<CampaignDetailResponse> {
    const token = sessionStorage.getItem('token');
    const headers = {
      Authorization: `Bearer ${token}`,
    };
    return this.http.put<CampaignDetailResponse>(
      `${this.campaignApiUrl}/Update-Campaign/${id}`,
      request,
      { headers }
    );
  }
}
