import api from "./api";
import type { BulkRegistroProductosRequest } from "./api-types";

export const InventoryAPI = {
  bulkProductos: (data: BulkRegistroProductosRequest) =>
    api.post('/api/Inventory/bulk-productos', data),
};