
import { Component, inject, signal } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    imports: [ReactiveFormsModule],
    styleUrl: './login.component.css'
})
export class LoginComponent {

    loginForm: FormGroup;

    private fb = inject(FormBuilder);
    authService = inject(AuthService);
    router = inject(Router);

    errorMessage = signal('');


    constructor() {
        this.loginForm = this.fb.group({
            username: ['', Validators.required],
            password: ['', Validators.required]
        });

    }

    get username() {
        return this.loginForm.get('username');
    }

    get password() {
        return this.loginForm.get('password');
    }

    async onSubmit() {
        if (this.loginForm.valid) {
            try {
                await this.authService.login(this.loginForm.value.username, this.loginForm.value.password);
                this.router.navigate(['/home'])
            } catch (error: any) {
                this.errorMessage.set(error.message); // Display the error message to the user
            }
        }
    }

    onLogout() {
        this.authService.logout();
    }
}