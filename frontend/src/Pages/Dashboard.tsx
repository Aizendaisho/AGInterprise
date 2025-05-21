import { useEffect, useState } from 'react';
import type { Producto } from '../api/api-types';
import { ProductoAPI } from '../api/producto';
import Navbar from '../Components/Navbar';
import ProductFormDialog from '../Components/ProductFormDialog';
// import { ProductoAPI } from '../api/api-client';
// import { Producto } from '../api/api-types';

const Dashboard = () => {
  const [productos, setProductos] = useState<Producto[]>([]);
  const [loading, setLoading] = useState(true);
  const [productoToEdit, setProductoToEdit] = useState<Producto | null>(null);
  const [productoIdToDelete, setProductoIdToDelete] = useState<number | null>(null);



  const loadProductos = () => {
    setLoading(true);
    ProductoAPI.getAll()
      .then((res) => {
        setProductos(res.data);
        setLoading(false);
      })
      .catch((error) => {
        console.error('Error al cargar productos:', error);
        setLoading(false);
      });
  };

  useEffect(() => {
    loadProductos();
  }, []);
const handleDelete = async (productoIdToDelete: number) => {
  if (productoIdToDelete === null) return;

  try {
    await ProductoAPI.delete(productoIdToDelete);
    setProductoIdToDelete(null);
    loadProductos();
    (document.getElementById('delete_modal') as HTMLDialogElement)?.close();
  } catch (error) {
    console.error('Error al eliminar el producto:', error);
  }
};


  return (<>
  <Navbar />
    <div className="p-4">
      <h1 className="text-2xl font-bold mb-4">Lista de Productos</h1>
      <button
  onClick={() => {
    const modal = document.getElementById('product_modal') as HTMLDialogElement;
    modal?.showModal();
  }}
  className="btn btn-primary"
>
  Crear Producto
</button>
<ProductFormDialog
  producto={productoToEdit ?? undefined}
  onSuccess={() => {
    setProductoToEdit(null); // Limpiar el estado
    loadProductos();
  }}
/>
<dialog id="delete_modal" className="modal">
  <div className="modal-box">
    <h3 className="font-bold text-lg">¿Eliminar producto?</h3>
    <p className="py-4">Esta acción eliminará el producto de forma permanente.</p>
    <div className="modal-action flex justify-end gap-2">
      <button
        className="btn"
        onClick={() => {
          setProductoIdToDelete(null);
          const modal = document.getElementById('delete_modal') as HTMLDialogElement;
          modal?.close();
        }}
      >
        Cancelar
      </button>
      <button
        className="btn btn-error text-white"
        onClick={() => handleDelete(productoIdToDelete!)}
      >
        Confirmar
      </button>
    </div>
  </div>
</dialog>


      {loading ? (
        <p>Cargando productos...</p>
      ) : (
        <div className="overflow-x-auto">
          <table className="table table-zebra w-full">
            <thead>
              <tr>
                <th>ID</th>
                <th>Nombre</th>
                <th>Categoría</th>
                <th>Unidad</th>
                <th>Precio</th>
              </tr>
            </thead>
            <tbody>
              {productos.map((producto) => (
<tr key={producto.id}>
  <td>{producto.id}</td>
  <td>{producto.nombre}</td>
  <td>{producto.categoria}</td>
  <td>{producto.unidadMedida}</td>
  <td>${producto.precioUnitario.toFixed(2)}</td>
  <td>
    <button
      className="btn btn-sm btn-outline"
      onClick={() => {
        setProductoToEdit(producto);
        const modal = document.getElementById('product_modal') as HTMLDialogElement;
        modal?.showModal();
      }}
    >
      Editar
    </button>
<button
  className="btn btn-sm btn-error text-white"
  onClick={() => {
    setProductoIdToDelete(producto.id);
    const modal = document.getElementById('delete_modal') as HTMLDialogElement;
    modal?.showModal();
  }}
>
  Eliminar
</button>

  </td>
</tr>

              ))}
            </tbody>
          </table>
        </div>
      )}
    </div>
      </>
  );
};

export default Dashboard;
