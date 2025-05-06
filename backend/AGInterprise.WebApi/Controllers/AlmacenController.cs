using AGInterprise.Application.Interfaces;
using AGInterprise.Domain.Entities.Almacenes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AGInterprise.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlmacenController : ControllerBase
{
    private readonly IAlmacenRepository _repo;

    public AlmacenController(IAlmacenRepository repo)
    {
        _repo = repo;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var almacenes = await _repo.ObtenerTodosAsync();
        return Ok(almacenes);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var almacen = await _repo.ObtenerPorIdAsync(id);
        return almacen is null ? NotFound() : Ok(almacen);
    }

    [Authorize(Roles = "Administrador")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Almacen almacen)
    {
        await _repo.CrearAsync(almacen);
        return CreatedAtAction(nameof(GetById), new { id = almacen.Id }, almacen);
    }

    [Authorize(Roles = "Administrador")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Almacen almacen)
    {
        if (id != almacen.Id)
            return BadRequest();

        await _repo.ActualizarAsync(almacen);
        return NoContent();
    }

    [Authorize(Roles = "Administrador")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repo.EliminarAsync(id);
        return NoContent();
    }
    
    
[HttpPatch("{id}/predeterminar")]
[Authorize(Roles = "Administrador")]
public async Task<IActionResult> SetPredeterminado(int id)
{
    // Quitar el flag del actual predeterminado
    var actual = await _repo.ObtenerTodosAsync();
    var pred = actual.FirstOrDefault(a => a.EsPredeterminado);
    if (pred is not null)
    {
        pred.EsPredeterminado = false;
        await _repo.ActualizarAsync(pred);
    }

    // Marcar el nuevo
    var nuevo = await _repo.ObtenerPorIdAsync(id);
    if (nuevo is null) return NotFound();
    nuevo.EsPredeterminado = true;
    await _repo.ActualizarAsync(nuevo);

    return NoContent();
}

}
