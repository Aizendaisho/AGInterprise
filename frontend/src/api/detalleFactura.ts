import api from "./api";
import type { DetalleFactura, DetalleFacturaRequest } from "./api-types";

export const DetalleFacturaAPI = {
  getAll: () => api.get<DetalleFactura[]>('/api/DetalleFactura'),
  getById: (id: number) => api.get<DetalleFactura>(`/api/DetalleFactura/${id}`),
  create: (data: DetalleFacturaRequest) => api.post('/api/DetalleFactura', data),
  update: (id: number, data: DetalleFactura) => api.put(`/api/DetalleFactura/${id}`, data),
  delete: (id: number) => api.delete(`/api/DetalleFactura/${id}`),
};