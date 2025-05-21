import { z } from 'zod';

export const productoSchema = z.object({
  nombre: z.string().min(1, 'Nombre requerido'),
  categoria: z.string().optional(),
  unidadMedida: z.string().optional(),
  precioUnitario: z.number().min(0.01, 'Precio debe ser mayor a 0'),
});

export type ProductoFormData = z.infer<typeof productoSchema>;
