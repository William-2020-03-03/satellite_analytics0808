import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

export interface UserLogAppInfo {
  Module: string;
  Operation: string;
  TriggerType: string;
  Browser: string;
  Build: string;
  Platform: string;
  Language: string;
  Created: Date;
}

@Injectable({
  providedIn: 'root'
})
export class BigDataService {

    private afterProxy_url = "/api/SatelliteAnalytics/application/GetBigDataByPaging";

      
    constructor(private http: HttpClient) {}


  loadData(skip:number, take: number) {
    return this.http.get<UserLogAppInfo[]>(`${this.afterProxy_url}?skip=${skip}&take=${take}`);
  }
}
