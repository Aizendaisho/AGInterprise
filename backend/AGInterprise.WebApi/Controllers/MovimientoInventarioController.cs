using AGInterprise.Application.Interfaces;
using AGInterprise.Domain.Entities.Inventario;
using AGInterprise.WebApi.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AGInterprise.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MovimientoInventarioController : ControllerBase
{
    private readonly IMovimientoInventarioService    _movService;
    private readonly IMovimientoInventarioRepository _movRepo;

    public MovimientoInventarioController(
        IMovimientoInventarioService    movService,
        IMovimientoInventarioRepository movRepo)
    {
        _movService = movService;
        _movRepo    = movRepo;
    }

    // POST usa el servicio
    [HttpPost]
    [Authorize(Roles = "Administrador,Vendedor")]
    public async Task<IActionResult> Create([FromBody] MovimientoInventarioRequest req)
    {
        // 1) Mapear DTO → Entidad de dominio
        var movimiento = new MovimientoInventario
        {
            Tipo             = req.Tipo,
            AlmacenOrigenId  = req.AlmacenOrigenId,
            AlmacenDestinoId = req.AlmacenDestinoId,
            Comentario       = req.Comentario,
            Detalles         = req.Detalles
                                   .Select(d => new DetalleMovimiento
                                   {
                                       ProductoId = d.ProductoId,
                                       Cantidad   = d.Cantidad
                                   }).ToList()
        };

        // 2) Llamar al servicio
        var creado = await _movService.ProcesarMovimientoAsync(movimiento);

        // 3) Devolver 201 y el recurso creado
        return CreatedAtAction(null, new { id = creado.Id }, creado);
    }


    // GET /api/movimientoinventario
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Get()
        => Ok(await _movRepo.ObtenerTodosAsync());

    // GET /api/movimientoinventario/{id}
    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(int id)
    {
        var m = await _movRepo.ObtenerPorIdAsync(id);
        return m is null ? NotFound() : Ok(m);
    }

    // PUT y DELETE igual, usando _movRepo
    [HttpPut("{id}")]
    [Authorize(Roles = "Administrador,Supervisor")]
    public async Task<IActionResult> Update(int id, [FromBody] MovimientoInventario movimiento)
    {
        if (id != movimiento.Id) return BadRequest();
        await _movRepo.ActualizarAsync(movimiento);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador")]
    public async Task<IActionResult> Delete(int id)
    {
        await _movRepo.EliminarAsync(id);
        return NoContent();
    }
}
