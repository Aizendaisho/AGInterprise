import api from "./api";
import type { MovimientoInventario, MovimientoInventarioRequest } from "./api-types";

export const MovimientoInventarioAPI = {
  getAll: () => api.get<MovimientoInventario[]>('/api/MovimientoInventario'),
  getById: (id: number) => api.get<MovimientoInventario>(`/api/MovimientoInventario/${id}`),
  create: (data: MovimientoInventarioRequest) => api.post('/api/MovimientoInventario', data),
  update: (id: number, data: MovimientoInventario) => api.put(`/api/MovimientoInventario/${id}`, data),
  delete: (id: number) => api.delete(`/api/MovimientoInventario/${id}`),
};