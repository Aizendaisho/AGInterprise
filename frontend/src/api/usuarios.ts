import api from "./api";

export const UsuariosAPI = {
  getAll: () => api.get('/api/Usuarios'),
  actualizarAlmacen: (id: number, almacenId: number) =>
    api.put(`/api/Usuarios/${id}/almacen`, almacenId),
};