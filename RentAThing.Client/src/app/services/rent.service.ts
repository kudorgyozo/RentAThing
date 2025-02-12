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
    type: ItemType;
}

export enum ItemType {
    Car = 1,
    Bike,
    ElectricScooter
}

@Injectable({
    providedIn: 'root'
})
export class RentService {

    private apiUrl = environment.apiUrl;  // Replace with your actual API URL
    http = inject(HttpClient);

    constructor() { }

    async getItems() {
        let values = await lastValueFrom(this.http.get<ItemDto[]>(this.apiUrl + '/rent'));
        return values;
    }

    async getActiveItems() {
        let values = await lastValueFrom(this.http.get<ItemDto[]>(this.apiUrl + '/profile/items/active'));
        return values;
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