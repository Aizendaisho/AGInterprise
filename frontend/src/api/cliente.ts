import api from "./api";
import type { Cliente } from "./api-types";

export const ClienteAPI = {
  getAll: () => api.get<Cliente[]>('/api/Cliente'),
  getById: (id: number) => api.get<Cliente>(`/api/Cliente/${id}`),
  create: (data: Cliente) => api.post('/api/Cliente', data),
  update: (id: number, data: Cliente) => api.put(`/api/Cliente/${id}`, data),
  delete: (id: number) => api.delete(`/api/Cliente/${id}`),
};