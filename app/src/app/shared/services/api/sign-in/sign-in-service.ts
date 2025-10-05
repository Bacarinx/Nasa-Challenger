import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class SignInService {
  constructor(private _httpClient: HttpClient) {}

  private readonly _URL: string = `${environment.apiUrl}/register/login`;
}
