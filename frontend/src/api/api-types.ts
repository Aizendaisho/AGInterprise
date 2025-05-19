export interface Ubicacion {
  id: number;
  almacenId: number;
  codigo?: string | null;
  descripcion?: string | null;
}

export interface Almacen {
  id: number;
  nombre?: string | null;
  descripcion?: string | null;
  direccion?: string | null;
  activo: boolean;
  fechaCreacion: string;
  fechaActualizacion?: string | null;
  esPredeterminado: boolean;
  ubicaciones?: Ubicacion[] | null;
}

export interface Producto {
  id: number;
  nombre?: string | null;
  codigoBarras?: string | null;
  categoria?: string | null;
  unidadMedida?: string | null;
  precioUnitario: number;
  fechaCreacion: string;
  fechaActualizacion?: string | null;
}

export interface Cliente {
  id: number;
  nombre?: string | null;
  rnc?: string | null;
  direccion?: string | null;
  telefono?: string | null;
  correo?: string | null;
}

export interface DetalleFactura {
  id: number;
  facturaId: number;
  productoId: number;
  cantidad: number;
  precioUnitario: number;
  total: number;
}

export interface DetalleFacturaRequest {
  productoId: number;
  cantidad: number;
  precioUnitario: number;
}

export interface Factura {
  id: number;
  clienteId: number;
  usuarioId: number;
  fecha: string;
  fechaFactura: string;
  tipoComprobante?: string | null;
  ncf?: string | null;
  total: number;
  estado?: string | null;
}

export interface FacturaRequest {
  clienteId: number;
  fechaFactura: string;
  detalles?: DetalleFacturaRequest[] | null;
}

export interface Inventario {
  id: number;
  productoId: number;
  almacenId: number;
  ubicacionId?: number | null;
  cantidad: number;
  stockMinimo: number;
  stockMaximo?: number | null;
  puntoReorden?: number | null;
}

export interface DetalleMovimientoRequest {
  productoId: number;
  cantidad: number;
}

export interface MovimientoInventarioRequest {
  tipo?: string | null;
  almacenOrigenId?: number | null;
  almacenDestinoId?: number | null;
  comentario?: string | null;
  detalles?: DetalleMovimientoRequest[] | null;
}

export interface MovimientoInventario {
  id: number;
  tipo?: string | null;
  almacenOrigenId?: number | null;
  almacenDestinoId?: number | null;
  fecha: string;
  comentario?: string | null;
}

export interface DetalleMovimiento {
  id: number;
  movimientoId: number;
  productoId: number;
  cantidad: number;
}

export interface BulkRegistroProductoDto {
  nombre?: string | null;
  categoria?: string | null;
  unidadMedida?: string | null;
  precioUnitario: number;
  cantidadInicial: number;
}

export interface BulkRegistroProductosRequest {
  almacenDestinoId: number;
  productos?: BulkRegistroProductoDto[] | null;
}

export interface LoginRequest {
  username?: string | null;
  password?: string | null;
}

export interface RegisterRequest {
  username?: string | null;
  email?: string | null;
  password?: string | null;
  nombre?: string | null;
  role?: string | null;
  almacenId?: number | null;
}
