using AGInterprise.Application.Interfaces;
using AGInterprise.Application.Models.Inventory;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class InventoryController : ControllerBase
{
    private readonly IBulkProductoService _bulkService;
    public InventoryController(IBulkProductoService bulkService)
    {
        _bulkService = bulkService;
    }

    [HttpPost("bulk-productos")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> BulkRegistrar([FromBody] BulkRegistroProductosRequest request)
    {
        await _bulkService.RegistrarProductosConStockAsync(request);
        return NoContent();
    }
}
