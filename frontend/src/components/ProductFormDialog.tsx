import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';

import { useEffect } from 'react';
import type { Producto } from '../api/api-types';
import { productoSchema, type ProductoFormData } from '../schemas/producto.schema';
import { ProductoAPI } from '../api/producto';
import z from 'zod';


const schema = z.object({
  nombre: z.string().min(1, 'El nombre es requerido'),
  categoria: z.string().optional(),
  unidadMedida: z.string().optional(),
  precioUnitario: z.coerce.number().min(0.01, 'Debe ser mayor a cero')
});

type FormData = z.infer<typeof schema>;

interface Props {
  producto?: Producto | null;
  onSuccess: () => void;
}

const ProductFormDialog = ({ producto, onSuccess }: Props) => {
  const {
    register,
    handleSubmit,
    reset,
    setValue,
    formState: { errors }
  } = useForm<FormData>({
    resolver: zodResolver(schema)
  });

  useEffect(() => {
    if (producto) {
      setValue('nombre', producto.nombre || '');
      setValue('categoria', producto.categoria || '');
      setValue('unidadMedida', producto.unidadMedida || '');
      setValue('precioUnitario', producto.precioUnitario);
    } else {
      reset();
    }
  }, [producto]);

  const onSubmit = async (data: FormData) => {
    try {
      if (producto) {
        await ProductoAPI.update(producto.id, {
          ...producto,
          ...data,
          fechaActualizacion: new Date().toISOString()
        });
      } else {
        await ProductoAPI.create({
          id: 0, // Dummy id, backend should assign the real id
          ...data,
          fechaCreacion: new Date().toISOString(),
          precioUnitario: Number(data.precioUnitario)
        });
      }
      onSuccess();
      const modal = document.getElementById('product_modal') as HTMLDialogElement;
      modal?.close();
    } catch (error) {
      console.error('Error al guardar producto:', error);
    }
  };

  return (
    <dialog id="product_modal" className="modal">
      <div className="modal-box">
        <h3 className="font-bold text-lg mb-4">
          {producto ? 'Editar Producto' : 'Crear Producto'}
        </h3>
        <form onSubmit={handleSubmit(onSubmit)} className="space-y-4">
          <div className="form-control">
            <label className="label">Nombre</label>
            <input {...register('nombre')} className="input input-bordered w-full" />
            {errors.nombre && <p className="text-red-500 text-sm">{errors.nombre.message}</p>}
          </div>

          <div className="form-control">
            <label className="label">Categoría</label>
            <input {...register('categoria')} className="input input-bordered w-full" />
          </div>

          <div className="form-control">
            <label className="label">Unidad de Medida</label>
            <input {...register('unidadMedida')} className="input input-bordered w-full" />
          </div>

          <div className="form-control">
            <label className="label">Precio Unitario</label>
            <input type="number" step="0.01" {...register('precioUnitario')} className="input input-bordered w-full" />
            {errors.precioUnitario && <p className="text-red-500 text-sm">{errors.precioUnitario.message}</p>}
          </div>

          <div className="modal-action">
            <button type="submit" className="btn btn-primary w-full">
              {producto ? 'Actualizar' : 'Crear'}
            </button>
          </div>
        </form>
      </div>
    </dialog>
  );
};



export default ProductFormDialog;
