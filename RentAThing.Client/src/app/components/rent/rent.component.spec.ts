import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RentComponent } from './rent.component';
import { provideExperimentalZonelessChangeDetection } from '@angular/core';
import { RentService } from '../../services/rent.service';
import { AuthService } from '../../services/auth.service';

describe('RentComponent', () => {
    let component: RentComponent;
    let fixture: ComponentFixture<RentComponent>;
    let rentServiceSpy: jasmine.SpyObj<RentService>;
    let authServiceSpy: jasmine.SpyObj<AuthService>;

    beforeEach(async () => {
        rentServiceSpy = jasmine.createSpyObj('RentService', ['startRent', 'stopRent', 'getItems']);
        authServiceSpy = jasmine.createSpyObj('AuthService', ['login', 'logout']);

        await TestBed.configureTestingModule({
            providers: [
                provideExperimentalZonelessChangeDetection(),
                { provide: RentService, useValue: rentServiceSpy },
                { provide: AuthService, useValue: authServiceSpy },
            ],
            imports: [RentComponent]
        })
            .compileComponents();

        fixture = TestBed.createComponent(RentComponent);
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
