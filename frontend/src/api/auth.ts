import api from "./api";
import type { LoginRequest, RegisterRequest } from "./api-types";

export const AuthAPI = {
  login: (data: LoginRequest) => api.post<{ token: string, userName: string, expiresAt:string  }>('/api/Auth/login', data),
  register: (data: RegisterRequest) => api.post('/api/Auth/register', data),
};