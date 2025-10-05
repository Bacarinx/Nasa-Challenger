import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of, switchMap } from 'rxjs';

import { environment } from 'src/environments/environment';

export interface SignUp {
  name: string;
  password: string;
  cpf: string;
  phoneNumber: string;
  birthDate: Date;
  street: string;
  number: string;
  neighborhood: string;
  city: string;
  country: string;
  respiratoryDiseasesIds: number[];
}

@Injectable({
  providedIn: 'root',
})
export class SignUpService {
  constructor(private _httpClient: HttpClient) {}

  private readonly _REGISTER_URL: string = `${environment.apiUrl}/register/register`;

  signUp(data: SignUp): Observable<any> {
    return this._httpClient.post(this._REGISTER_URL, data);
  }
}
