// src/store/useAuthStore.ts
import { create } from 'zustand'

interface AuthState {
  token: string | null;
  user: any;
  setAuth: (token: string, user: any) => void;
  logout: () => void;
}

export const useAuthStore = create<AuthState>((set) => ({
  token: null,
  user: null,

  setAuth: (token, user) => {
    set({ token, user });
    localStorage.setItem('auth_token', token); // Por si acaso también aquí
  },

  logout: () => {
    localStorage.removeItem('auth_token'); // ✅ elimina el token del localStorage
    set({ token: null, user: null });
  },
}));
