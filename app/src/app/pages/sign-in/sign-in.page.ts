import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {
  SignIn,
  SignInService,
} from 'src/app/shared/services/api/sign-in/sign-in-service';
import { JwtService } from 'src/app/shared/services/local/jwt/jwt-service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.page.html',
  styleUrls: ['./sign-in.page.scss'],
  standalone: false,
})
export class SignInPage implements OnInit {
  signInData: SignIn = {
    cpf: '',
    password: '',
  };

  showPassword = false;

  constructor(
    private readonly _signInService: SignInService,
    private readonly _jwtService: JwtService,
    private readonly _router: Router,
  ) {}

  ngOnInit() {}

  signIn(): void {
    this._signInService.signIn(this.signInData).subscribe({
      next: (response) => {
        console.log(response);

        this._jwtService.set(response.token);

        this._router.navigate(['home']);
      },
    });
  }

  togglePassword() {
    this.showPassword = !this.showPassword;
  }
}
