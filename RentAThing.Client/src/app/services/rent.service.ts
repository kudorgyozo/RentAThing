import { inject, Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, lastValueFrom, Observable, throwError } from 'rxjs';
import { environment } from '../../environments/environment';

export interface ItemDto {
    id: number;
    name: string;
    pricePerHour: number;
    renter?: string;
    rentStart?: string; // UTC date string
    type: string;
}

@Injectable({
    providedIn: 'root'
})
export class RentService {

    private apiUrl = environment.apiUrl;  // Replace with your actual API URL
    http = inject(HttpClient);

    constructor() { }

    getItems() {
        return lastValueFrom(this.http.get<ItemDto[]>(this.apiUrl + '/rent'));
    }

    startRent(id: number) {
        const url = `${this.apiUrl}/rent/${id}/start`;
        return lastValueFrom(this.http.put(url, null));
    }

    stopRent(id: number) {
        const url = `${this.apiUrl}/rent/${id}/stop`;
        return lastValueFrom(this.http.put(url, null));
    }
}