
import axios from 'axios';
import type { Almacen, BulkRegistroProductosRequest, LoginRequest, RegisterRequest } from './api-types';

const api = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL || 'http://localhost:5198',
  headers: {
    'Content-Type': 'application/json',
  },
});

api.interceptors.request.use((config) => {
  const token = localStorage.getItem('auth_token');
  if (token) {
    if (!config.headers) {
      config.headers = {};
    }
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export const AlmacenAPI = {
  getAll: () => api.get('/api/Almacen'),
  getById: (id: number) => api.get(`/api/Almacen/${id}`),
  create: (data: Almacen) => api.post('/api/Almacen', data),
  update: (id: number, data: Almacen) => api.put(`/api/Almacen/${id}`, data),
  delete: (id: number) => api.delete(`/api/Almacen/${id}`),
  setDefault: (id: number) => api.patch(`/api/Almacen/${id}/predeterminar`)
};

export const AuthAPI = {
  login: (data: LoginRequest) => api.post('/api/Auth/login', data),
  register: (data: RegisterRequest) => api.post('/api/Auth/register', data)
};

export const ClienteAPI = {
  getAll: () => api.get('/api/Cliente'),
  getById: (id: number) => api.get(`/api/Cliente/${id}`),
  create: (data: Almacen) => api.post('/api/Cliente', data),
  update: (id: number, data: Almacen) => api.put(`/api/Cliente/${id}`, data),
  delete: (id: number) => api.delete(`/api/Cliente/${id}`)
};

export const ProductoAPI = {
  getAll: () => api.get('/api/Producto'),
  getById: (id: number) => api.get(`/api/Producto/${id}`),
  create: (data: Almacen) => api.post('/api/Producto', data),
  update: (id: number, data: Almacen) => api.put(`/api/Producto/${id}`, data),
  delete: (id: number) => api.delete(`/api/Producto/${id}`)
};

// Puedes seguir este patrón para las otras entidades: Factura, DetalleFactura, Inventario, MovimientoInventario, etc.

export default api;

export const FacturaAPI = {
  getAll: () => api.get('/api/Factura'),
  getById: (id: number) => api.get(`/api/Factura/${id}`),
  create: (data: Almacen) => api.post('/api/Factura', data),
  update: (id: number, data: Almacen) => api.put(`/api/Factura/${id}`, data),
  delete: (id: number) => api.delete(`/api/Factura/${id}`)
};

export const DetalleFacturaAPI = {
  getAll: () => api.get('/api/DetalleFactura'),
  getById: (id: number) => api.get(`/api/DetalleFactura/${id}`),
  create: (data: Almacen) => api.post('/api/DetalleFactura', data),
  update: (id: number, data: Almacen) => api.put(`/api/DetalleFactura/${id}`, data),
  delete: (id: number) => api.delete(`/api/DetalleFactura/${id}`)
};

export const InventarioAPI = {
  getAll: () => api.get('/api/Inventario'),
  getByProductoYAlmacen: (productoId: number, almacenId: number) =>
    api.get(`/api/Inventario/${productoId}/${almacenId}`)
};

export const InventoryAPI = {
  bulkProductos: (data: BulkRegistroProductosRequest) => api.post('/api/Inventory/bulk-productos', data)
};

export const MovimientoInventarioAPI = {
  getAll: () => api.get('/api/MovimientoInventario'),
  getById: (id: number) => api.get(`/api/MovimientoInventario/${id}`),
  create: (data: Almacen) => api.post('/api/MovimientoInventario', data),
  update: (id: number, data: Almacen) => api.put(`/api/MovimientoInventario/${id}`, data),
  delete: (id: number) => api.delete(`/api/MovimientoInventario/${id}`)
};

export const DetalleMovimientoAPI = {
  getAll: () => api.get('/api/DetalleMovimiento'),
  getById: (id: number) => api.get(`/api/DetalleMovimiento/${id}`),
  create: (data: Almacen) => api.post('/api/DetalleMovimiento', data),
  update: (id: number, data: Almacen) => api.put(`/api/DetalleMovimiento/${id}`, data),
  delete: (id: number) => api.delete(`/api/DetalleMovimiento/${id}`)
};

export const UsuariosAPI = {
  getAll: () => api.get('/api/Usuarios'),
  actualizarAlmacen: (id: number, almacenId: number) =>
    api.put(`/api/Usuarios/${id}/almacen`, almacenId)
};
