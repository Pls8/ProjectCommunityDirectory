import { Component } from '@angular/core';
import { AuthService } from '../../services/auth';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-login',
  imports: [FormsModule],
  standalone: true,
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class LoginComponent {
  model = { email: '', password: '' };

  constructor(
    private auth: AuthService,
    private router: Router
  ) { }

  error = '';
  loading = false;
  
  login() {
    this.auth.login(this.model).subscribe({
      next: () => {
        this.router.navigateByUrl('/');
      },
      error: err => {
        this.error = err.error?.message ?? 'Login failed';
        this.loading = false;
      }
    });
  }
}
