import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Campaign } from '../../models/Campaign';
import { catchError, Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CampaignService {

  campaignApiUrl = "http://localhost:5221/api/Campaigns";

  constructor(private http: HttpClient) { }

  getAllCampaigns(): Observable<any> {
    return this.http.get<any>(this.campaignApiUrl).pipe(
      catchError((error) => {
        return throwError(() => new Error(error.message));
      })
    );
  }

  getTotalCampaignCount():Observable<any>{
    return this.http.get<any>(this.campaignApiUrl+"/TotalCampaignsCount").pipe(
      catchError((error) => {
        return throwError(() => new Error(error.message));
      })
    );
  }
}
