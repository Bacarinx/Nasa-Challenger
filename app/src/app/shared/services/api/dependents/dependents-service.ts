import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment.prod';

import { JwtService } from '../../local/jwt/jwt-service';

export interface Requester {
  id: number;
  cpf: string;
  name: string;
  phoneNumber: string;
}

export interface ConnectionRequest {
  id: number;
  requester: Requester;
  targetId: number;
  accepted: boolean;
  requestedAt: string;
}

@Injectable({
  providedIn: 'root',
})
export class DependentsService {
  constructor(
    private _httpClient: HttpClient,
    private _jwtService: JwtService,
  ) {}

  private readonly _URL: string = `${environment.apiUrl}/dependents`;

  add(cpf: string): Observable<any> {
    return this._httpClient.post(`${this._URL}/add-dependent`, cpf);
  }

  accept(id: number): Observable<any> {
    let token: string = this._jwtService.get() ?? 'token';

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });

    return this._httpClient.post(
      `${this._URL}/accept-request/${id}`,
      {},
      {
        headers: headers,
      },
    );
  }

  getAll(): Observable<ConnectionRequest[]> {
    let token: string = this._jwtService.get() ?? 'token';

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });

    return this._httpClient.get<ConnectionRequest[]>(`${this._URL}`, {
      headers: headers,
    });
  }
}
