import { Component, inject, signal } from '@angular/core';
import { ItemDto, RentService } from '../../services/rent.service';
import { AsyncPipe, DatePipe } from '@angular/common';
import { interval, map, Observable, Subject, takeUntil } from 'rxjs';
import { AuthService } from '../../services/auth.service';

@Component({
    selector: 'app-rent',
    imports: [DatePipe, AsyncPipe],
    templateUrl: './rent.component.html',
    styleUrl: './rent.component.css'
})
export class RentComponent {
    rentService = inject(RentService);
    authService = inject(AuthService);
    items = signal<ItemDto[]>([]);





    async ngOnInit() {
        this.loadItems();
    }

    async startRent(id: number) {
        await this.rentService.startRent(id);
        this.loadItems();
    }

    async stopRent(id: number) {
        await this.rentService.stopRent(id);
        this.loadItems();
    }

    async loadItems() {
        this.items.set(await this.rentService.getItems());
    }
}
