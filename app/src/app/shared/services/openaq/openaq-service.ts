import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Observable, of, forkJoin } from 'rxjs';
import { switchMap, map, catchError } from 'rxjs/operators';
import { environment } from 'src/environments/environment.prod';

export interface AirQualityDataPoint {
  value: number;
  coordinates: {
    latitude: number;
    longitude: number;
  };
}

interface OpenAQLocationsResponse {
  results: { id: number }[];
}

interface OpenAQLatestResponse {
  results: AirQualityDataPoint[];
}

@Injectable({
  providedIn: 'root',
})
export class OpenaqService {
  private readonly apiUrl = '/openaq-api/v3';
  private readonly apiKey = environment.openaqApiKey;

  constructor(private http: HttpClient) {}

  public getMapData(
    lat: number,
    lon: number,
    precision: number,
  ): Observable<AirQualityDataPoint[]> {
    return this.getLocationIds(lat, lon, precision).pipe(
      switchMap((ids) => {
        if (ids.length === 0) {
          return of([]);
        }
        const latestDataRequests = ids.map((id) =>
          this.getLatestDataForLocation(id),
        );
        return forkJoin(latestDataRequests);
      }),
      map((responses) => responses.reduce((acc, val) => acc.concat(val), [])),
    );
  }

  private getLatestDataForLocation(
    locationId: number,
  ): Observable<AirQualityDataPoint[]> {
    const headers = new HttpHeaders({ 'X-API-Key': this.apiKey });
    const url = `${this.apiUrl}/locations/${locationId}/latest`;
    const params = new HttpParams().set('limit', 1);

    return this.http.get<OpenAQLatestResponse>(url, { headers, params }).pipe(
      map((response) => response.results),
      catchError(() => of([])),
    );
  }

  private getLocationIds(
    lat: number,
    lon: number,
    precision: number,
  ): Observable<number[]> {
    const coords1 = this.offsetCoord(lat, lon, -precision, -precision);
    const coords2 = this.offsetCoord(lat, lon, precision, precision);

    const headers = new HttpHeaders({ 'X-API-Key': this.apiKey });
    const url = `${this.apiUrl}/locations`;
    const params = new HttpParams()
      .set('parameters_id', 2)
      .set(
        'bbox',
        `${coords1.lon},${coords1.lat},${coords2.lon},${coords2.lat}`,
      )
      .set('limit', 20);

    return this.http
      .get<OpenAQLocationsResponse>(url, { headers, params })
      .pipe(
        map((response) => response.results.map((element) => element.id)),
        catchError(() => of([])),
      );
  }

  private offsetCoord(
    lat: number,
    lon: number,
    dx: number,
    dy: number,
  ): { lat: number; lon: number } {
    const dLat = dy / 111320;
    const dLon = dx / (111320 * Math.cos((Math.PI / 180) * lat));
    const newLat = lat + dLat;
    const newLon = lon + dLon;
    return { lat: newLat, lon: newLon };
  }
}
