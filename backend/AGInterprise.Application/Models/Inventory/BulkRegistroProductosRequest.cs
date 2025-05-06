using System.Collections.Generic;

namespace AGInterprise.Application.Models.Inventory;

public class BulkRegistroProductosRequest
{
    public int AlmacenDestinoId { get; set; }
    public List<BulkRegistroProductoDto> Productos { get; set; } = new();
}
