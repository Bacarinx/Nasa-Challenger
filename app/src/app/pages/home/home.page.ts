import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { GoogleMap } from '@angular/google-maps';
import { Geolocation } from '@capacitor/geolocation';
import { Subscription } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import {
  AirQualityDataPoint,
  OpenaqService,
} from 'src/app/shared/services/openaq/openaq-service';

export interface AqiInfo {
  className: string;
  label: string;
  color: string;
}

@Component({
  selector: 'app-home',
  templateUrl: './home.page.html',
  styleUrls: ['./home.page.scss'],
  standalone: false,
})
export class HomePage implements OnInit, OnDestroy {
  @ViewChild(GoogleMap) map!: GoogleMap;

  private mapIdleSubscription!: Subscription;

  initialCenter: google.maps.LatLngLiteral = { lat: -23.501, lng: -47.458 }; // Sorocaba
  options: google.maps.MapOptions = {
    mapId: 'DEMO_MAP_ID',
    zoom: 12,
    center: this.initialCenter,
    streetViewControl: false,
    mapTypeControl: false,
  };
  userMarkerPosition!: google.maps.LatLngLiteral;
  airQualityPoints: AirQualityDataPoint[] = [];

  constructor(private readonly openaqService: OpenaqService) {}

  ngOnInit() {
    this.getCurrentLocation();
  }

  ngAfterViewInit(): void {
    this.mapIdleSubscription = this.map.idle
      .pipe(debounceTime(500))
      .subscribe(() => {
        this.fetchAirQualityData();
      });
  }

  async getCurrentLocation() {
    try {
      const coordinates = await Geolocation.getCurrentPosition({
        enableHighAccuracy: true,
      });
      const userLocation = {
        lat: coordinates.coords.latitude,
        lng: coordinates.coords.longitude,
      };
      this.userMarkerPosition = userLocation;
      this.map.googleMap?.setCenter(userLocation);
    } catch (error) {
      console.error('Error getting location', error);
    }
  }

  fetchAirQualityData() {
    if (!this.map?.googleMap?.getBounds()) return;
    const center = this.map.googleMap.getCenter()!.toJSON();
    const bounds = this.map.googleMap.getBounds();
    const northEast = bounds!.getNorthEast();
    const radius = google.maps.geometry.spherical.computeDistanceBetween(
      center,
      northEast,
    );
    this.openaqService
      .getMapData(center.lat, center.lng, radius)
      .subscribe((data) => {
        this.airQualityPoints = data.map((point) => ({
          ...point,
          value: Math.round(point.value),
        }));
      });
  }

  getAqiInfo(value: number): AqiInfo {
    if (value <= 50)
      return { className: 'aqi-good', label: 'Good', color: '#00e400' };
    if (value <= 100)
      return { className: 'aqi-moderate', label: 'Moderate', color: '#ffff00' };
    if (value <= 150)
      return {
        className: 'aqi-unhealthy-sensitive',
        label: 'Unhealthy for Sensitive Groups',
        color: '#ff7e00',
      };
    if (value <= 200)
      return {
        className: 'aqi-unhealthy',
        label: 'Unhealthy',
        color: '#ff0000',
      };
    if (value <= 300)
      return {
        className: 'aqi-very-unhealthy',
        label: 'Very Unhealthy',
        color: '#8f3f97',
      };
    return { className: 'aqi-hazardous', label: 'Hazardous', color: '#7e0023' };
  }

  getCircleOptions(point: AirQualityDataPoint): google.maps.CircleOptions {
    const aqiInfo = this.getAqiInfo(point.value);
    return {
      strokeColor: aqiInfo.color,
      strokeOpacity: 0.9,
      strokeWeight: 2,
      fillColor: aqiInfo.color,
      fillOpacity: 0.4,
      center: {
        lat: point.coordinates.latitude,
        lng: point.coordinates.longitude,
      },
      radius: 5000,
    };
  }

  recenterMap() {
    this.getCurrentLocation();
  }

  ngOnDestroy(): void {
    if (this.mapIdleSubscription) {
      this.mapIdleSubscription.unsubscribe();
    }
  }
}
