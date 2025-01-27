import { HttpClient } from '@angular/common/http';
import { Component, OnInit, signal } from '@angular/core';
import { NgIf, NgFor } from '@angular/common';

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
})
export class AppComponent implements OnInit {
    public forecasts = signal<WeatherForecast[]>([]);

    constructor(private http: HttpClient) { }

    ngOnInit() {
        this.getForecasts();
    }

    getForecasts() {
        this.http.get<WeatherForecast[]>('/weatherforecast').subscribe(
            (result) => {
                this.forecasts.set(result);
            },
            (error) => {
                console.error(error);
            }
        );
    }

    title = 'RentAThing.Client';
}
