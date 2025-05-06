using AGInterprise.Application.Interfaces;
using AGInterprise.Domain.Entities.Inventario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AGInterprise.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DetalleMovimientoController : ControllerBase
{
    private readonly IDetalleMovimientoRepository _repo;

    public DetalleMovimientoController(IDetalleMovimientoRepository repo)
    {
        _repo = repo;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var detalles = await _repo.ObtenerTodosAsync();
        return Ok(detalles);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var detalle = await _repo.ObtenerPorIdAsync(id);
        return detalle is null ? NotFound() : Ok(detalle);
    }

    [Authorize(Roles = "Administrador, Supervisor")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DetalleMovimiento detalle)
    {
        await _repo.CrearAsync(detalle);
        return CreatedAtAction(nameof(GetById), new { id = detalle.Id }, detalle);
    }

    [Authorize(Roles = "Administrador")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] DetalleMovimiento detalle)
    {
        if (id != detalle.Id)
            return BadRequest();

        await _repo.ActualizarAsync(detalle);
        return NoContent();
    }

    [Authorize(Roles = "Administrador")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repo.EliminarAsync(id);
        return NoContent();
    }
}
