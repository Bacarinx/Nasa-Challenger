import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { IonicModule, MenuController } from '@ionic/angular';

@Component({
  selector: 'app-sidebar-menu',
  templateUrl: './sidebar-menu.component.html',
  styleUrls: ['./sidebar-menu.component.scss'],
  imports: [IonicModule, CommonModule, RouterModule],
})
export class SidebarMenuComponent {
  public appPages = [
    { title: 'Home', url: '/home', icon: 'home' },
    { title: 'Scan QR Code', url: '/scan-qr-code', icon: 'scan' },
    { title: 'My QR Code', url: '/qr-code', icon: 'qr-code' },
    { title: 'Contacts', url: '/contacts', icon: 'people' },
    { title: 'Sign In', url: '/sign-in', icon: 'log-in' },
    { title: 'Sign Up', url: '/sign-up', icon: 'person-add' },
  ];

  constructor(private menuCtrl: MenuController) {}

  closeMenu() {
    this.menuCtrl.close();
  }
}
