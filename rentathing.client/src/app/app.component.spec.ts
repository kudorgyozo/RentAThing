import { HttpClientTestingModule, HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { ComponentFixture, TestBed, tick } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { provideHttpClient } from '@angular/common/http';
import { provideExperimentalZonelessChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './routes';

describe('AppComponent', () => {
    let component: AppComponent;
    let fixture: ComponentFixture<AppComponent>;
    let httpMock: HttpTestingController;

    beforeEach(async () => {
        await TestBed.configureTestingModule({
            providers: [
                provideExperimentalZonelessChangeDetection(),
                provideRouter(routes),
                provideHttpClient(),
                provideHttpClientTesting()
            ],
            imports: [AppComponent]
        }).compileComponents();

        fixture = TestBed.createComponent(AppComponent);
        await fixture.whenStable();
        component = fixture.componentInstance;
        httpMock = TestBed.inject(HttpTestingController);
    });

    afterEach(() => {
        httpMock.verify();
    });

    it('should create the app', () => {
        expect(component).toBeTruthy();
        const req = httpMock.expectOne('/api/weatherforecast');
        httpMock.verify();
    });

    it('should retrieve weather forecasts from the server', async () => {
        const mockForecasts = [
            { date: '2021-10-01', temperatureC: 20, temperatureF: 68, summary: 'Mild' },
            { date: '2021-10-02', temperatureC: 25, temperatureF: 77, summary: 'Warm' }
        ];

        expect(true).toBe(true);

        const req = httpMock.expectOne('/api/weatherforecast');
        expect(req.request.method).toEqual('GET');
        req.flush(mockForecasts);

        // Wait for the signal to stabilize
        //fixture.detectChanges();
        await fixture.whenStable();

        expect(component.forecasts()).toEqual(mockForecasts);
    });
});