import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { environment } from 'src/environments/environment';

export interface SignIn {
  cpf: string;
  password: string;
}

@Injectable({
  providedIn: 'root',
})
export class SignInService {
  constructor(private _httpClient: HttpClient) {}

  private readonly _URL: string = `${environment.apiUrl}/register/login`;

  signIn(data: SignIn): Observable<any> {
    return this._httpClient.post(this._URL, data);
  }
}
