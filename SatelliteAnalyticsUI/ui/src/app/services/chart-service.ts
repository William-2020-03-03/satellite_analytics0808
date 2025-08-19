import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

export interface Top3ByModuleAndOperation {
  Module: string;
  Operation: string;
  Count: number;
}

@Injectable({
  providedIn: 'root'
})
export class ChartService {
     private apiUrl = 'https://localhost:7159/api/SatelliteAnalytics/application/Top3ByModuleAndOperation';

     private apiUrl2 = "http://localhost:5288/api/SatelliteAnalytics/application/Top3ByModuleAndOperation";

    private apiUrl3 = "http://localhost:5000/api/SatelliteAnalytics/application/Top3ByModuleAndOperation";


    private afterProxy_url = "/api/SatelliteAnalytics/application/Top3ByModuleAndOperation";

    private afterProxy_url_for_redis = "/api/Redis/application/Redis/Top3ByModuleAndOperation";

  constructor(private http: HttpClient) {

  }
  
  getTop3ByModuleAndOperation(): Observable<Top3ByModuleAndOperation[]> {
    return this.http.get<Top3ByModuleAndOperation[]>(this.afterProxy_url);
  }


  getTop3ByModuleAndOperationFromRedis(): Observable<Top3ByModuleAndOperation[]> {
    return this.http.get<Top3ByModuleAndOperation[]>(this.afterProxy_url_for_redis);
  }



  //   getTop3ByModuleAndOperation(): Observable<Top3ByModuleAndOperation[]> {
  //   return this.http.get<Top3ByModuleAndOperation[]>(this.apiUrl);
  // }
}
