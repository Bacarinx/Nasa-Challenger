import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment.prod';

import { JwtService } from '../../local/jwt/jwt-service';

interface UserData {
  id: number;
  name: string;
  cpf: string;
  country: string;
  state: string;
  city: string;
  street: string;
  number: string;
  neighborhood: string;
}

@Injectable({
  providedIn: 'root',
})
export class UserService {
  constructor(
    private _httpClient: HttpClient,
    private _jwtService: JwtService,
  ) {}

  private readonly _URL: string = `${environment.apiUrl}/register`;

  get(): Observable<UserData> {
    let token: string = this._jwtService.get() ?? 'token';

    const headers = new HttpHeaders({
      Authorization: `Bearer ${token}`,
    });

    return this._httpClient.get<UserData>(`${this._URL}/GetUser`, {
      headers,
    });
  }
}
