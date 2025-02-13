import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AppComponent } from './app.component';
import { provideHttpClient } from '@angular/common/http';
import { provideExperimentalZonelessChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

describe('AppComponent', () => {
    let component: AppComponent;
    let fixture: ComponentFixture<AppComponent>;
    let httpMock: HttpTestingController;
    //let routerSpy: jasmine.SpyObj<Router>;

    beforeEach(async () => {
        //routerSpy = jasmine.createSpyObj('Router', ['navigate', 'navigateByUrl']);

        await TestBed.configureTestingModule({
            providers: [
                provideExperimentalZonelessChangeDetection(),
                //{ provide: Router, useValue: routerSpy },
                provideRouter([]),
                provideHttpClient(),
                provideHttpClientTesting()
            ],
            imports: [AppComponent,
                //RouterTestingModule
            ]
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