
import { Component, inject, OnInit, signal } from '@angular/core';
import { FormGroup, FormBuilder, Validators, ReactiveFormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    imports: [ReactiveFormsModule],
    styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {

    loginForm: FormGroup;

    private fb = inject(FormBuilder);
    authService = inject(AuthService);
    errorMessage = signal('');

    constructor() {
        this.loginForm = this.fb.group({
            username: ['admin', Validators.required],
            password: ['admin', Validators.required]
        });

    }

    ngOnInit(): void {
    }

    get username() {
        return this.loginForm.get('username');
    }

    get password() {
        return this.loginForm.get('password');
    }

    onSubmit(): void {
        if (this.loginForm.valid) {
            this.authService.login(this.loginForm.value.username, this.loginForm.value.password).subscribe({
                next: () => {
                    // Handle successful login (e.g., navigate to dashboard)
                },
                error: (err: Error) => {
                    this.errorMessage.set(err.message); // Display the error message to the user
                }
            });
        }
    }

    onLogout() {
        this.authService.logout();
    }
}