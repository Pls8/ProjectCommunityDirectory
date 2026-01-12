import { Component } from '@angular/core';
import { AuthService } from '../../services/auth';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-register',
  imports: [FormsModule, CommonModule],
  standalone: true,
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class RegisterComponent {
  model = {
    fullName: '',
    email: '',
    password: '',
    confirmPassword: ''
  };

  error = '';
  loading = false;

  constructor(private auth: AuthService, private router: Router) { }

  register() {
    this.error = '';
    this.loading = true;

    if (this.model.password !== this.model.confirmPassword) {
      this.error = 'Passwords do not match';
      this.loading = false;
      return;
    }

    // this.auth.register(this.model).subscribe({
    //   next: () => {
    //     this.router.navigateByUrl('/');
    //   },
    //   error: err => {
    //     this.error = err.error?.message ?? 'Registration failed';
    //     this.loading = false;
    //   }
    // });
    this.auth.register(this.model).subscribe({
      next: () => {
        this.router.navigateByUrl('/');
      },
      error: err => {
        this.error =
          err.error?.message ||
          err.error?.errors?.ConfirmPassword?.[0] ||
          'Registration failed';
        this.loading = false;
      }
    });
  }
}
