import { Component, inject, signal } from '@angular/core';
import { ItemDto, RentService, ItemType } from '../../services/rent.service';
import { AuthService } from '../../services/auth.service';
import { RentItemComponent } from './rent-item/rent-item.component';

@Component({
    selector: 'app-rent',
    imports: [RentItemComponent],
    templateUrl: './rent.component.html',
    styleUrl: './rent.component.css'
})
export class RentComponent {
    rentService = inject(RentService);
    authService = inject(AuthService);
    items = signal<ItemDto[]>([]);

    itemType = ItemType; // Type assign

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
