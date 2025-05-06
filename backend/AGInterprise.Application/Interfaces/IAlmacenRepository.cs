using AGInterprise.Domain.Entities.Almacenes;

namespace AGInterprise.Application.Interfaces;

public interface IAlmacenRepository
{
    Task<IEnumerable<Almacen>> ObtenerTodosAsync();
    Task<Almacen?> ObtenerPorIdAsync(int id);
    Task CrearAsync(Almacen almacen);
    Task ActualizarAsync(Almacen almacen);
    Task EliminarAsync(int id);
    Task<Almacen?> ObtenerPredeterminadoAsync();

}
