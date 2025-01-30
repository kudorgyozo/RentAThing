import { TestBed } from '@angular/core/testing';

import { AuthService } from './auth.service';
import { provideExperimentalZonelessChangeDetection } from '@angular/core';
import { provideHttpClient } from '@angular/common/http';
import { HttpTestingController, provideHttpClientTesting } from '@angular/common/http/testing';

describe('AuthService', () => {
    let service: AuthService;
    let httpMock: HttpTestingController;


    beforeEach(() => {
        TestBed.configureTestingModule({
            providers: [
                //AuthService,
                provideExperimentalZonelessChangeDetection(),
                provideHttpClient(),
                provideHttpClientTesting()
            ]
        });
        service = TestBed.inject(AuthService);
        httpMock = TestBed.inject(HttpTestingController);
    });

    afterEach(() => {
        httpMock.verify();
    });

    it('should be created', () => {
        expect(service).toBeTruthy();
    });

    it('should login successfully', async () => {
        const username = 'user1';
        const password = 'user1';

        const promise = service.login(username, password);

        const req = httpMock.expectOne('https://localhost:57024/api/user/login')
        req.flush({
            token: 'mock-token',
            claims: {
                name: username,
            }
        });

        await promise;

        expect(service.token).toBe('mock-token');
        expect(service.username()).toBe(username);
    });

    it('should handle login error', async () => {
        const username = 'invaliduser';
        const password = 'wrongpassword';

        const promise = service.login(username, password);

        const req = httpMock.expectOne('https://localhost:57024/api/user/login');
        req.flush('Unauthorized', { status: 401, statusText: 'Unauthorized' });

        await expectAsync(promise)
            .toBeRejectedWith(new Error('Invalid credentials. Please check your username and password.'));
    });

});
