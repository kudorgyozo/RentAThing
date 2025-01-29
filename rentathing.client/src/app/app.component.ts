import { HttpClient } from '@angular/common/http';
import { Component, OnInit, signal } from '@angular/core';
import { NgIf, NgFor } from '@angular/common';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./components/header/header.component";

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

    constructor(private http: HttpClient) { }

    ngOnInit() {
        //this.getForecasts();
    }

    getForecasts() {
        this.http.get<WeatherForecast[]>('/api/weatherforecast').subscribe({
            next: (result) => {
                this.forecasts.set(result);
            },
            error: (error) => {
                console.error(error);
            }
        });
    }

    title = 'RentAThing.Client';
}
