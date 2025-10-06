import { Component } from '@angular/core';
import { BarcodeScanner } from '@capacitor-mlkit/barcode-scanning';
import { PluginListenerHandle } from '@capacitor/core';
import { AlertController } from '@ionic/angular';
import { DependentsService } from 'src/app/shared/services/api/dependents/dependents-service';

@Component({
  selector: 'app-scan-qr-code',
  templateUrl: './scan-qr-code.page.html',
  styleUrls: ['./scan-qr-code.page.scss'],
  standalone: false,
})
export class ScanQrCodePage {
  scanListener: PluginListenerHandle | null = null;
  scanCompleted = false;
  isScanning = false;

  constructor(
    private _alertController: AlertController,
    private _dependentsService: DependentsService,
  ) {}

  ionViewWillLeave() {
    this.stopScan();
  }

  resetScan(): void {
    this.scanCompleted = false;
  }

  async startScan(): Promise<void> {
    const granted = await this.requestPermissions();
    if (!granted) {
      await this.presentAlert(
        'Permission Denied',
        'Camera permission is required.',
      );
      return;
    }

    this.isScanning = true;
    document.querySelector('body')?.classList.add('scanner-active');

    this.scanListener = await BarcodeScanner.addListener(
      'barcodesScanned',
      async (result) => {
        if (result.barcodes.length > 0) {
          const barcode = result.barcodes[0];
          console.log('QR Code detected:', barcode.displayValue);

          await this.stopScan();

          const cpf = barcode.rawValue;

          this._dependentsService.add(cpf).subscribe({
            next: () => {
              this.scanCompleted = true;
            },
          });

          this.scanCompleted = true;
        }
      },
    );

    await BarcodeScanner.startScan();
  }

  async stopScan(): Promise<void> {
    if (this.scanListener) {
      await this.scanListener.remove();
      this.scanListener = null;
    }

    await BarcodeScanner.stopScan();

    this.isScanning = false;
    document.querySelector('body')?.classList.remove('scanner-active');
  }

  async requestPermissions(): Promise<boolean> {
    const { camera } = await BarcodeScanner.requestPermissions();
    return camera === 'granted' || camera === 'limited';
  }

  async presentAlert(header: string, message: string): Promise<void> {
    const alert = await this._alertController.create({
      header,
      message,
      buttons: ['OK'],
    });
    await alert.present();
  }
}
