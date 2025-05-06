// AGInterprise.Application/Interfaces/IInventarioRepository.cs

using AGInterprise.Domain.Entities.Inventario;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AGInterprise.Application.Interfaces
{
    public interface IInventarioRepository
    {
        Task<IEnumerable<Inventario>> ObtenerTodosAsync();              // ← nuevo
        Task<Inventario?> ObtenerPorProductoAlmacenAsync(int productoId, int almacenId);
        Task ActualizarAsync(Inventario inventario);
    }
}
