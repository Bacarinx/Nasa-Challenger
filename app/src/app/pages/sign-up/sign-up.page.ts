import { Component, OnInit } from '@angular/core';
import { Route, Router } from '@angular/router';
import {
  RespiratoryDisease,
  RespiratoryDiseasesService,
} from 'src/app/shared/services/api/respiratory-diseases/respiratory-diseases-service';
import {
  SignUp,
  SignUpService,
} from 'src/app/shared/services/api/sign-up/sign-up-service';
import { JwtService } from 'src/app/shared/services/local/jwt/jwt-service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.page.html',
  styleUrls: ['./sign-up.page.scss'],
  standalone: false,
})
export class SignUpPage implements OnInit {
  signUpData: SignUp = {
    name: '',
    password: '',
    cpf: '',
    phoneNumber: '',
    birthDate: new Date(),
    street: '',
    number: '',
    neighborhood: '',
    city: '',
    country: '',
    respiratoryDiseasesIds: [],
  };

  isChecked: boolean[] = [];
  respiratoryDiseases: RespiratoryDisease[] = [];

  constructor(
    private readonly _respiratoryDiseasesService: RespiratoryDiseasesService,
    private readonly _signUpService: SignUpService,
    private readonly _jwtService: JwtService,
    private readonly _router: Router,
  ) {}

  ngOnInit() {
    this._respiratoryDiseasesService.getAll().subscribe({
      next: (response) => {
        this.respiratoryDiseases = response;

        this.isChecked = this.respiratoryDiseases.map(() => false);
      },
    });
  }

  signUp(): void {
    for (let i = 0; i < this.respiratoryDiseases.length; i++) {
      if (this.isChecked[i]) {
        this.signUpData.respiratoryDiseasesIds.push(
          this.respiratoryDiseases[i].id,
        );
      }
    }

    this._signUpService.signUp(this.signUpData).subscribe({
      next: (response) => {
        console.log(response);

        this._jwtService.set(response.token);

        this._router.navigate(['home']);
      },
    });
  }

  cancel(): void {}
}
