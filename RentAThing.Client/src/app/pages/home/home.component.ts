import { Component } from '@angular/core';
import { RentComponent } from "../../components/rent/rent.component";

@Component({
    selector: 'app-home',
    imports: [RentComponent],
    standalone: true,
    templateUrl: './home.component.html',
    styleUrl: './home.component.css'
})
export class HomeComponent {

}
