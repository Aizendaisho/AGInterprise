import api from "./api";
import type { DetalleMovimiento, DetalleMovimientoRequest } from "./api-types";

export const DetalleMovimientoAPI = {
  getAll: () => api.get<DetalleMovimiento[]>('/api/DetalleMovimiento'),
  getById: (id: number) => api.get<DetalleMovimiento>(`/api/DetalleMovimiento/${id}`),
  create: (data: DetalleMovimientoRequest) => api.post('/api/DetalleMovimiento', data),
  update: (id: number, data: DetalleMovimiento) => api.put(`/api/DetalleMovimiento/${id}`, data),
  delete: (id: number) => api.delete(`/api/DetalleMovimiento/${id}`),
};