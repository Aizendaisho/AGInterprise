import axios from 'axios';
import { useAuthStore } from '../store/useAuthStore';

// Crear instancia de axios
const api = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL || 'http://localhost:5198',
  headers: {
    'Content-Type': 'application/json',
  },
});

// Interceptor para incluir el token automáticamente
api.interceptors.request.use((config) => {
  const token = useAuthStore.getState().token || localStorage.getItem('auth_token');

  if (token) {
    if (!config.headers) {
      config.headers = {};
    }
    config.headers.Authorization = `Bearer ${token}`;
  }

  return config;
}, (error) => {
  return Promise.reject(error);
});

export default api;
