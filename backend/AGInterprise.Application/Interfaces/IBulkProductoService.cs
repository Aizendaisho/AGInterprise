using AGInterprise.Application.Models.Inventory;

namespace AGInterprise.Application.Interfaces;

public interface IBulkProductoService
{
    Task RegistrarProductosConStockAsync(BulkRegistroProductosRequest request);
}
