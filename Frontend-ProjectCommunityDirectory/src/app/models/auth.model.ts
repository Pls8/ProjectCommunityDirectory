export interface LoginDto {
  email: string;
  password: string;
}

export interface RegisterDto {
  fullName: string;
  email: string;
  password: string;
  confirmPassword: string;
}

export interface User {
  id: string;
  email: string;
  fullName: string;
  roles: string[];
}

export interface AuthResponse {
  success: boolean;
  token: string;
  expiration: string;
  user: User;
}
