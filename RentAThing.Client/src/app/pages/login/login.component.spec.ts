import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoginComponent } from './login.component';
import { provideHttpClient } from '@angular/common/http';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { provideExperimentalZonelessChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from '../../routes';
import { AuthService } from '../../services/auth.service';

describe('LoginComponent', () => {
    let component: LoginComponent;
    let fixture: ComponentFixture<LoginComponent>;
    let authServiceSpy: jasmine.SpyObj<AuthService>;

    beforeEach(async () => {
        const spy = jasmine.createSpyObj('AuthService', ['login', 'logout']);
        await TestBed.configureTestingModule({
            providers: [
                provideExperimentalZonelessChangeDetection(),
                provideRouter(routes),
                { provide: AuthService, useValue: spy }
            ],
            imports: [LoginComponent]
        }).compileComponents();

        fixture = TestBed.createComponent(LoginComponent);
        authServiceSpy = TestBed.inject(AuthService) as jasmine.SpyObj<AuthService>;
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });
});
