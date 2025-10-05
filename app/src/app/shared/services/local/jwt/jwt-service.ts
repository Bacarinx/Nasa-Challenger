import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class JwtService {
  private readonly _JWT_KEY = 'JWT_TOKEN';

  set(token: string): void {
    localStorage.setItem(this._JWT_KEY, token);
  }

  get(): string | null {
    return localStorage.getItem(this._JWT_KEY);
  }

  isSignedIn(): boolean {
    console.log(this.get());
    return this.get() != null;
  }
}
