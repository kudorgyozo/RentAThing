import { Component, EventEmitter, input, Input, output, Output } from '@angular/core';
import { ItemDto, ItemType } from '../../../services/rent.service';
import { DatePipe } from '@angular/common';
import { AuthService } from '../../../services/auth.service';

@Component({
    selector: 'li[app-rent-item]',
    imports: [DatePipe],
    standalone: true,
    templateUrl: './rent-item.component.html',
    styleUrl: './rent-item.component.css',
    host: {
        class: 'bg-gray-100 p-4 rounded-lg shadow-md flex justify-between items-center'
    }
})
export class RentItemComponent {
    item = input<ItemDto>({
        id: 1,
        name: '',
        pricePerHour: 0,
        type: ItemType.Car
    });
    startRent = output<number>();
    stopRent = output<number>();

    //@Output() startRent = new EventEmitter<number>();
    //@Output() stopRent = new EventEmitter<number>();

    itemType = ItemType; // Type assign

    constructor(public authService: AuthService) { }

    onStartRent(id: number) {
        this.startRent.emit(id);
    }

    onStopRent(id: number) {
        this.stopRent.emit(id);
    }
}
