import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class SearchPlaces {
  constructor(private _httpClient: HttpClient) {}

  private readonly _URL: string =
    'https://places.googleapis.com/v1/places:searchText';
}
