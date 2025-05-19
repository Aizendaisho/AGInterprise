import React, { useEffect, useState } from 'react';
import type { Producto } from '../api/api-types';
import { ProductoAPI } from '../api/producto';
import Navbar from '../Components/Navbar';
// import { ProductoAPI } from '../api/api-client';
// import { Producto } from '../api/api-types';

const Dashboard = () => {
  const [productos, setProductos] = useState<Producto[]>([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    ProductoAPI.getAll()
      .then((res) => {
        setProductos(res.data);
        setLoading(false);
      })
      .catch((error) => {
        console.error('Error al cargar productos:', error);
        setLoading(false);
      });
  }, []);

  return (<>
  <Navbar />
    <div className="p-4">
      <h1 className="text-2xl font-bold mb-4">Lista de Productos</h1>

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
