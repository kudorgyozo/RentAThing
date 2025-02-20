// auth.service.ts
import { inject, Injectable, signal } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Router } from '@angular/router';
import { firstValueFrom } from 'rxjs';
import { LoggingService } from './logging.service';

interface LoginResponse {
    token: string;
    claims: Record<string, string>;
    expires: number;
}

interface LoginData {
    token: string;
    username: string;
    expires: number;
}

const LoginDataKey = 'loginData';
const apiUrl = environment.apiUrl + '/user/login';

@Injectable({ providedIn: 'root' })
export class AuthService {

    username = signal<string | null>(null);
    token?: string;

    private http = inject(HttpClient);
    private router = inject(Router);
    private logging = inject(LoggingService);

    constructor() {
        const loginDataStr = localStorage.getItem(LoginDataKey);
        if (loginDataStr) {
            const { token, username, expires } = JSON.parse(loginDataStr);
            const now = new Date();
            const expireDate = new Date(expires * 1000);
            if (now > expireDate) {
                this.cleanup();
                return;
            }

            //this.scheduleLogout(expireDate);

            this.username.set(username);
            this.token = token;
        }

    }

    private scheduleLogout(expireDate: Date) {
        const delayms = expireDate.getTime() - (new Date().getTime());
        setTimeout(() => {
            this.logging.debug('Automatic logout, token expired')
            this.cleanup();
        }, delayms);
    }

    async login(username: string, password: string, redirectUrl?: string) {
        this.logging.debug('login');
        try {
            const response = await firstValueFrom(this.http.post<LoginResponse>(apiUrl, { username, password }));
            this.logging.debug('login response', response);
            // Save the token and username to localStorage
            this.username.set(response.claims['name']);
            this.token = response.token;

            const loginData: LoginData = {
                token: response.token, username: response.claims['name'], expires: response.expires
            }
            localStorage.setItem(LoginDataKey, JSON.stringify(loginData));

            //this.scheduleLogout(new Date(response.expires * 1000));

            if (redirectUrl) {
                this.router.navigate([redirectUrl]);
            }

        } catch (error: unknown) {
            this.cleanup();

            this.logging.warn('login error', error);
            let errorMessage = 'An error occurred. Please try again.';
            if (error instanceof HttpErrorResponse && error.status === 401) {
                errorMessage = 'Invalid credentials. Please check your username and password.';
            } else if (error instanceof HttpErrorResponse && error.status === 500) {
                errorMessage = 'Server error. Please try again later.';
            }

            throw new Error(errorMessage);

        }
    }

    logout() {
        this.cleanup();
    }

    private cleanup() {
        this.username.set(null);
        this.token = undefined;
        localStorage.removeItem(LoginDataKey);
    }
}