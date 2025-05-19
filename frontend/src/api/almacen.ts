// src/api/almacen.ts
import api from './api';
import type { Almacen } from './api-types';


export const AlmacenAPI = {
  getAll: () => api.get<Almacen[]>('/api/Almacen'),
  getById: (id: number) => api.get<Almacen>(`/api/Almacen/${id}`),
  create: (data: Almacen) => api.post('/api/Almacen', data),
  update: (id: number, data: Almacen) => api.put(`/api/Almacen/${id}`, data),
  delete: (id: number) => api.delete(`/api/Almacen/${id}`),
  setDefault: (id: number) => api.patch(`/api/Almacen/${id}/predeterminar`)
};
