import api from "./api";
import type { Factura, FacturaRequest } from "./api-types";

export const FacturaAPI = {
  getAll: () => api.get<Factura[]>('/api/Factura'),
  getById: (id: number) => api.get<Factura>(`/api/Factura/${id}`),
  create: (data: FacturaRequest) => api.post('/api/Factura', data),
  update: (id: number, data: Factura) => api.put(`/api/Factura/${id}`, data),
  delete: (id: number) => api.delete(`/api/Factura/${id}`),
};