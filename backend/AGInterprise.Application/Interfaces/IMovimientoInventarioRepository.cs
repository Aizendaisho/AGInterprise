using AGInterprise.Domain.Entities.Inventario;

namespace AGInterprise.Application.Interfaces;

public interface IMovimientoInventarioRepository
{
    Task<IEnumerable<MovimientoInventario>> ObtenerTodosAsync();
    Task<MovimientoInventario?> ObtenerPorIdAsync(int id);
    Task CrearAsync(MovimientoInventario movimiento);
    Task ActualizarAsync(MovimientoInventario movimiento);
    Task EliminarAsync(int id);
}
