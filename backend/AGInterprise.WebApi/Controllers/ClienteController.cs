using AGInterprise.Application.Interfaces;
using AGInterprise.Domain.Entities.Facturacion;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AGInterprise.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClienteController : ControllerBase
{
    private readonly IClienteRepository _repo;

    public ClienteController(IClienteRepository repo)
    {
        _repo = repo;
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var clientes = await _repo.ObtenerTodosAsync();
        return Ok(clientes);
    }

    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var cliente = await _repo.ObtenerPorIdAsync(id);
        return cliente is null ? NotFound() : Ok(cliente);
    }

    [Authorize (Roles = "Administrador,Vendedor")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Cliente cliente)
    {
        await _repo.CrearAsync(cliente);
        return CreatedAtAction(nameof(GetById), new { id = cliente.Id }, cliente);
    }

    [Authorize (Roles = "Administrador,Vendedor")]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] Cliente cliente)
    {
        if (id != cliente.Id)
            return BadRequest();

        await _repo.ActualizarAsync(cliente);
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
