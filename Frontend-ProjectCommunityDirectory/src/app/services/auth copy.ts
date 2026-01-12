import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { AuthResponse, LoginDto, RegisterDto } from '../models/auth.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private baseUrl = `${environment.apiUrl}/Account`;

  
  constructor(private http: HttpClient) {}

  login(data: LoginDto) {
    return this.http.post<AuthResponse>(`${this.baseUrl}/login`, data);
  }

  register(data: RegisterDto) {
    return this.http.post<AuthResponse>(`${this.baseUrl}/register`, data);
  }

  saveAuth(response: AuthResponse) {
    localStorage.setItem('token', response.token);
    localStorage.setItem('user', JSON.stringify(response.user));
  }

  getUser() {
    return JSON.parse(localStorage.getItem('user')!);
  }

  isAdmin(): boolean {
    return this.getUser()?.roles.includes('Admin');
  }

  logout() {
    localStorage.clear();
  }
}
