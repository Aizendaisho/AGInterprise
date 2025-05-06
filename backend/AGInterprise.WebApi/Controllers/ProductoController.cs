using AGInterprise.Application.Interfaces;
using AGInterprise.Domain.Entities.Inventario;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AGInterprise.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductoController : ControllerBase
{
    private readonly IProductoRepository _repo;

    public ProductoController(IProductoRepository repo)
    {
        _repo = repo;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var productos = await _repo.ObtenerTodosAsync();
        return Ok(productos);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var producto = await _repo.ObtenerPorIdAsync(id);
        return producto is null ? NotFound() : Ok(producto);
    }

    [Authorize (Roles = "Administrador")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Producto producto)
    {
        await _repo.CrearAsync(producto);
        return CreatedAtAction(nameof(GetById), new { id = producto.Id }, producto);
    }

    [Authorize (Roles = "Administrador")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Producto producto)
    {
        if (id != producto.Id)
            return BadRequest();

        await _repo.ActualizarAsync(producto);
        return NoContent();
    }

    [Authorize (Roles = "Administrador")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repo.EliminarAsync(id);
        return NoContent();
    }
}
