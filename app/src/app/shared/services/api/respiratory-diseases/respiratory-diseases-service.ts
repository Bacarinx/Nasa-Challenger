import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment';

export interface RespiratoryDisease {
  id: number;
  disease: string;
}

@Injectable({
  providedIn: 'root',
})
export class RespiratoryDiseasesService {
  constructor(private _httpClient: HttpClient) {}

  private readonly _URL: string = `${environment.apiUrl}/RespiratoryDieses`;

  getAll(): Observable<RespiratoryDisease[]> {
    return this._httpClient.get<RespiratoryDisease[]>(this._URL);
  }
}
