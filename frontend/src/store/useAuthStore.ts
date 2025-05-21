// src/store/useAuthStore.ts
import { create } from 'zustand'

interface AuthState {
  token: string | null;
  userName: string | null;
  expiresAt: string | null;
  setAuth: (token: string, userName: string, expiresAt: string) => void;
  logout: () => void;
}

export const useAuthStore = create<AuthState>((set) => ({
  token: localStorage.getItem('auth_token'),
  userName: localStorage.getItem('auth_user'),
  expiresAt: localStorage.getItem('auth_expiresAt'),

  setAuth: (token, userName, expiresAt) => {
    set({ token, userName, expiresAt });
    localStorage.setItem('auth_token', token);
    localStorage.setItem('auth_user', userName);
    localStorage.setItem('auth_expiresAt', expiresAt);
  },

  logout: () => {
    localStorage.removeItem('auth_token');
    localStorage.removeItem('auth_user');
    localStorage.removeItem('auth_expiresAt');
    set({ token: null, userName: null, expiresAt: null });
  },
}));

