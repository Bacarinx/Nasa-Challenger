import { Component, OnInit } from '@angular/core';
import { UserService } from 'src/app/shared/services/api/user/user-service';

@Component({
  selector: 'app-qr-code',
  templateUrl: './qr-code.page.html',
  styleUrls: ['./qr-code.page.scss'],
  standalone: false,
})
export class QrCodePage implements OnInit {
  qrCodeValue: string = 'empty';

  constructor(private _userService: UserService) {}

  ngOnInit() {
    this._userService.get().subscribe({
      next: (response) => {
        this.qrCodeValue = response.cpf;
      },
    });
  }
}
