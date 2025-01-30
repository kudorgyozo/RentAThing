import { HttpClient } from '@angular/common/http';
import { Component, inject, OnInit, signal } from '@angular/core';
import { NgIf, NgFor } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./components/header/header.component";
import { lastValueFrom } from 'rxjs';
import { LoggingService } from './services/logging.service';

interface WeatherForecast {
    date: string;
    temperatureC: number;
    temperatureF: number;
    summary: string;
}

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrl: './app.component.css',
    imports: [RouterOutlet, HeaderComponent]
})
export class AppComponent implements OnInit {
    public forecasts = signal<WeatherForecast[]>([]);
    title = 'RentAThing.Client';

    http = inject(HttpClient);
    logging = inject(LoggingService);

    constructor() { }

    ngOnInit() {
        this.logging.debug('init');

        this.getForecasts();
    }

    async getForecasts() {
        this.logging.debug('getforecasts');
        const result = await lastValueFrom(this.http.get<WeatherForecast[]>('/api/weatherforecast'));
        //console.log(result);
        this.forecasts.set(result);
        this.logging.debug('getforecasts result', result);
    }

}
