import { Component, OnInit } from '@angular/core';
import {
  ConnectionRequest,
  DependentsService,
} from 'src/app/shared/services/api/dependents/dependents-service';

export interface Contact {
  name: string;
  phone: string;
}

@Component({
  selector: 'app-contacts',
  templateUrl: './contacts.page.html',
  styleUrls: ['./contacts.page.scss'],
  standalone: false,
})
export class ContactsPage implements OnInit {
  accepted: ConnectionRequest[] = [];
  toAccept: ConnectionRequest[] = [];

  contacts: Contact[] = [
    {
      name: 'Bombeiros',
      phone: '193',
    },
  ];

  constructor(private _dependentsService: DependentsService) {}

  update(): void {
    this._dependentsService.getAll().subscribe({
      next: (response) => {
        this.accepted = response.filter((r) => r.accepted);

        this.toAccept = response.filter((r) => !r.accepted);

        console.log(response);
        console.log(this.accepted);
        console.log(this.toAccept);
      },
    });
  }

  ngOnInit() {
    this.update();
  }

  accept(id: number): void {
    this._dependentsService.accept(id).subscribe({
      next: (response) => {
        console.log(response);

        this.update();
      },
      error: () => {
        this.update();
      },
    });
  }
}
