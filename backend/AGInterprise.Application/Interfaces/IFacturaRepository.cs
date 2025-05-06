using AGInterprise.Domain.Entities.Facturacion;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AGInterprise.Application.Interfaces
{
    public interface IFacturaRepository
    {
        Task<IEnumerable<Factura>> ObtenerTodasAsync();
        Task<Factura?> ObtenerPorIdAsync(int id);
        Task CrearAsync(Factura factura);
        Task ActualizarAsync(Factura factura);
        Task EliminarAsync(int id);
    }
}
