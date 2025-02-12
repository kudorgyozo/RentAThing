import { Component, inject, signal } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { ItemDto, RentService } from '../../services/rent.service';
import { RentItemComponent } from '../../components/rent/rent-item/rent-item.component';

@Component({
    selector: 'app-profile',
    imports: [RentItemComponent],
    templateUrl: './profile.component.html',
    styleUrl: './profile.component.css'
})
export class ProfileComponent {


    authService = inject(AuthService);
    rentService = inject(RentService);
    items = signal<ItemDto[]>([]);

    async ngOnInit() {
        this.items.set(await this.rentService.getActiveItems());
    }

    async stopRent(id: number) {
        await this.rentService.stopRent(id);
        this.items.set(await this.rentService.getActiveItems());
    }

    startRent(id: number) {

    }
}
