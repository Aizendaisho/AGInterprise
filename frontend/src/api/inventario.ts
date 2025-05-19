import api from "./api";
import type { Inventario } from "./api-types";

export const InventarioAPI = {
  getAll: () => api.get<Inventario[]>('/api/Inventario'),
  getByProductoYAlmacen: (productoId: number, almacenId: number) =>
    api.get<Inventario>(`/api/Inventario/${productoId}/${almacenId}`),
};
