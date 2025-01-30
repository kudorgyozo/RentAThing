import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LoginComponent } from './login.component';
import { provideExperimentalZonelessChangeDetection } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { FormBuilder } from '@angular/forms';

describe('LoginComponent', () => {
    let component: LoginComponent;
    let fixture: ComponentFixture<LoginComponent>;
    let authServiceSpy: jasmine.SpyObj<AuthService>;
    let routerSpy: jasmine.SpyObj<Router>;

    beforeEach(async () => {
        authServiceSpy = jasmine.createSpyObj('AuthService', ['login', 'logout']);
        routerSpy = jasmine.createSpyObj('Router', ['navigate']);

        await TestBed.configureTestingModule({
            providers: [
                provideExperimentalZonelessChangeDetection(),
                { provide: AuthService, useValue: authServiceSpy },
                { provide: Router, useValue: routerSpy },
                FormBuilder,
            ],
            imports: [LoginComponent]
        }).compileComponents();

        fixture = TestBed.createComponent(LoginComponent);
        //authServiceSpy = TestBed.inject(AuthService) as jasmine.SpyObj<AuthService>;
        component = fixture.componentInstance;
        fixture.detectChanges();
    });

    it('should create', () => {
        expect(component).toBeTruthy();
    });

    it('should initialize form with username and password controls', () => {
        expect(component.loginForm.contains('username')).toBeTruthy();
        expect(component.loginForm.contains('password')).toBeTruthy();
    });

    it('should invalidate the form when empty', () => {
        component.loginForm.setValue({ username: '', password: '' });

        expect(component.loginForm.valid).toBeFalsy();
    });

    it('should call authService.login and navigate on successful login', async () => {
        //authServiceSpy.login.and.returnValue(Promise.resolve());
        authServiceSpy.login.and.returnValue(Promise.resolve())
        component.loginForm.setValue({ username: 'test', password: 'password' });

        await component.onSubmit();

        expect(authServiceSpy.login).toHaveBeenCalledWith('test', 'password');
        expect(routerSpy.navigate).toHaveBeenCalledWith(['/home']);
    });

    it('should set errorMessage on login failure', async () => {
        const errorMsg = 'Invalid credentials';
        component.loginForm.setValue({ username: 'test', password: 'wrong' });
        authServiceSpy.login.and.returnValue(Promise.reject(new Error(errorMsg)));

        await component.onSubmit();

        expect(component.errorMessage()).toBe(errorMsg);
    });
});
