import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { AuthResponse, LoginDto, RegisterDto } from '../models/auth.model';
import { BehaviorSubject, tap } from 'rxjs'; 

@Injectable({
  providedIn: 'root',
})
export class AuthService {

  private baseUrl = `${environment.apiUrl}/Account`;

  private currentUserSource = new BehaviorSubject<any | null>(null);
  currentUser$ = this.currentUserSource.asObservable();

  constructor(private http: HttpClient) {
    this.loadUserFromStorage();
  }

  private loadUserFromStorage() {
    const token = localStorage.getItem('token');
    const user = localStorage.getItem('user');

    if (token && user) {
      this.currentUserSource.next(JSON.parse(user));
    }
  }

  private handleAuthSuccess(res: AuthResponse) {
    localStorage.setItem('token', res.token);
    localStorage.setItem('user', JSON.stringify(res.user));
    this.currentUserSource.next(res.user);
  }

  login(data: LoginDto) {
    return this.http
      .post<AuthResponse>(`${this.baseUrl}/login`, data)
      .pipe(tap(res => this.handleAuthSuccess(res)));
  }

  register(data: RegisterDto) {
    return this.http
      .post<AuthResponse>(`${this.baseUrl}/register`, data)
      .pipe(tap(res => this.handleAuthSuccess(res)));
  }

  logout() {
    localStorage.clear();
    this.currentUserSource.next(null);
  }

  isLoggedIn(): boolean {
    return !!this.currentUserSource.value;
  }

  isAdmin(): boolean {
    return this.currentUserSource.value?.roles?.includes('Admin');
  }

  getUser() {
    return this.currentUserSource.value;
  }
}
