import { useProductos } from '../hooks/useProductos';

export function ListaProductos() {
  const { data, isLoading, error } = useProductos() as { data: { id: string; nombre: string; precioUnitario: number }[] | undefined, isLoading: boolean, error: any };
  if (isLoading) return <p>Cargando…</p>;
  if (error) return <p>Error al cargar productos</p>;
  return (
    <ul>
      {data?.map(p => (
        <li key={p.id}>{p.nombre} – {p.precioUnitario}</li>
      ))}
    </ul>
  );
}
