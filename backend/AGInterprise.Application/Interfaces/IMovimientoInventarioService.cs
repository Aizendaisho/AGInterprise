using AGInterprise.Domain.Entities.Inventario;

namespace AGInterprise.Application.Interfaces;

public interface IMovimientoInventarioService
{
    /// <summary>
    /// Registra un movimiento (Entrada/Salida/Transferencia),
    /// sus detalles y actualiza el inventario en el mismo almacén(es).
    /// </summary>
    Task<MovimientoInventario> ProcesarMovimientoAsync(MovimientoInventario movimiento);
}
