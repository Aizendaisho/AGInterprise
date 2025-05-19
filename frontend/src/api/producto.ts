import api from "./api";
import type { Producto } from "./api-types";

export const ProductoAPI = {
  getAll: () => api.get<Producto[]>('/api/Producto'),
  getById: (id: number) => api.get<Producto>(`/api/Producto/${id}`),
  create: (data: Producto) => api.post('/api/Producto', data),
  update: (id: number, data: Producto) => api.put(`/api/Producto/${id}`, data),
  delete: (id: number) => api.delete(`/api/Producto/${id}`),
};