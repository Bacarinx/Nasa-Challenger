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

  emergencyContacts = [
    {
      name: 'Fire Dept. / Paramedics',
      phone: '911',
      details: 'For immediate respiratory distress or rescue.',
      icon: 'flame-outline', // Icon for fire department
      color: 'danger',
    },
    {
      name: 'Poison Control',
      phone: '1-800-222-1222',
      details: 'For exposure to airborne toxins or chemicals.',
      icon: 'flask-outline',
      color: 'warning',
    },
    {
      name: 'EPA Environmental Violations',
      phone: '1-800-424-8802',
      details: 'Report major air or water pollution.',
      icon: 'leaf-outline',
      color: 'success',
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

  getInitials(name: string): string {
    if (!name) {
      return '';
    }
    const nameParts = name.trim().split(' ');
    if (nameParts.length === 1) {
      return nameParts[0].charAt(0).toUpperCase();
    }
    const firstNameInitial = nameParts[0].charAt(0);
    const lastNameInitial = nameParts[nameParts.length - 1].charAt(0);
    return (firstNameInitial + lastNameInitial).toUpperCase();
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

  reject(id: number) {
    console.log(`Rejected request with ID: ${id}`);
  }
}
