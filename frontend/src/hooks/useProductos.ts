// src/hooks/useProductos.ts
import { useQuery } from '@tanstack/react-query';
import { api } from '../api/api';

export function useProductos() {
  return useQuery({ queryKey: ['productos'], queryFn: () => api.productoGET() });
}
