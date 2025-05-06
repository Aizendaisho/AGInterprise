using System.Security.Claims;
using AGInterprise.Application.Interfaces;
using AGInterprise.Domain.Entities.Facturacion;
using AGInterprise.WebApi.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AGInterprise.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FacturaController : ControllerBase
{
    private readonly ILogger<FacturaController> _logger;

    private readonly IFacturaRepository _factRepo;
    private readonly IFacturaService    _factService;

    public FacturaController(IFacturaRepository factRepo,
                             IFacturaService    factService,
                             ILogger<FacturaController> logger)
    {
        _factRepo     = factRepo;
        _factService  = factService;
         _logger      = logger; 
    }

    // Listar todas las facturas con el repositorio
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get()
        => Ok(await _factRepo.ObtenerTodasAsync());

    // Obtener por ID con el repositorio
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(int id)
    {
        var f = await _factRepo.ObtenerPorIdAsync(id);
        return f is null ? NotFound() : Ok(f);
    }

    // El Create pasa a usar el servicio con la lógica completa
  [HttpPost]
    [Authorize(Roles = "Administrador,Vendedor")]
    public async Task<IActionResult> Create([FromBody] FacturaRequest req)
    {
        _logger.LogInformation("Creando factura para cliente {ClienteId} a las {Fecha}", req.ClienteId, req.FechaFactura);

        // 1) Extraer userId del JWT
        var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(claim, out var userId))
            return Forbid();

        // 2) Mapear DTO → Entidad de dominio
        var factura = new Factura
        {
            ClienteId    = req.ClienteId,
            FechaFactura = req.FechaFactura,
            Detalles     = req.Detalles
                                .Select(d => new DetalleFactura
                                {
                                    ProductoId     = d.ProductoId,
                                    Cantidad       = d.Cantidad,
                                    PrecioUnitario = d.PrecioUnitario
                                }).ToList()
        };

        // 3) Llamar al servicio
        var creada = await _factService.CrearFacturaCompletaAsync(factura, userId);

        // 4) Devolver 201 con ubicación
        return CreatedAtAction(nameof(GetById), new { id = creada.Id }, creada);
    }


    [Authorize(Roles = "Administrador")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Factura factura)
    {
        if (id != factura.Id)
            return BadRequest();

        await _factRepo.ActualizarAsync(factura);
        return NoContent();
    }

    [Authorize(Roles = "Administrador")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _factRepo.EliminarAsync(id);
        return NoContent();
    }
}
