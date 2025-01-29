// auth.service.ts
import { inject, Injectable, signal } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { environment } from '../../environments/environment';
import { Router } from '@angular/router';
import { firstValueFrom, throwError } from 'rxjs';

interface LoginResponse {
    token: string;
    claims: Record<string, string>;
}

const LoginDataKey = 'loginData';

@Injectable({ providedIn: 'root' })
export class AuthService {

    token: string | null = null;
    username = signal<string | null>(null);

    private apiUrl = environment.apiUrl + '/user/login';
    private http = inject(HttpClient);
    private router = inject(Router);

    constructor() {
        const loginData = localStorage.getItem(LoginDataKey);
        if (loginData) {
            const { token, username } = JSON.parse(loginData);
            this.username.set(username);
            this.token = token;
        }

    }

    async login(username: string, password: string, redirectUrl?: string) {

        try {
            const response = await firstValueFrom(this.http.post<LoginResponse>(this.apiUrl, { username, password }));

            // Save the token and username to localStorage
            this.username.set(response.claims['name']);
            this.token = response.token;

            localStorage.setItem(LoginDataKey, JSON.stringify({ token: response.token, username: response.claims['name'] }));
            if (redirectUrl) {
                this.router.navigate([redirectUrl]);
            }

        } catch (error: any) {
            let errorMessage = 'An error occurred. Please try again.';
            if (error.status === 401) {
                errorMessage = 'Invalid credentials. Please check your username and password.';
            } else if (error.status === 500) {
                errorMessage = 'Server error. Please try again later.';
            }

            throw new Error(errorMessage);

        }
    }

    logout() {
        this.username.set(null);
        this.token = null;
        localStorage.removeItem(LoginDataKey);
    }
}