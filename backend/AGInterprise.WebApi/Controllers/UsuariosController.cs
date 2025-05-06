// WebApi/Controllers/UsuariosController.cs
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AGInterprise.Domain.Entities.Seguridad;
using AGInterprise.Application.Interfaces;
using AGInterprise.WebApi.Models;

namespace AGInterprise.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrador")]    // Solo Admin puede acceder
    public class UsuariosController : ControllerBase
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly IFacturaRepository   _facturaRepo;

        public UsuariosController(
            UserManager<Usuario> userManager,
            IFacturaRepository   facturaRepo)
        {
            _userManager   = userManager;
            _facturaRepo   = facturaRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            // 1) Obtener todos los usuarios
            var usuarios = await Task.Run(() => _userManager.Users.ToList());

            // 2) Obtener todas las facturas
            var todasFacturas = await _facturaRepo.ObtenerTodasAsync();

            // 3) Construir el DTO para cada usuario
            var result = new List<UsuarioWithFacturasDto>();
            foreach (var u in usuarios)
            {
                var roles = await _userManager.GetRolesAsync(u);
                var facturasUsuario = todasFacturas
                    .Where(f => f.UsuarioId == u.Id)
                    .ToList();

#pragma warning disable CS8601 // Possible null reference assignment.
                result.Add(new UsuarioWithFacturasDto {
                    Id        = u.Id,
                    UserName  = u.UserName,
                    Email     = u.Email,
                    Roles     = roles,
                    Facturas  = facturasUsuario
                });
#pragma warning restore CS8601 // Possible null reference assignment.
            }

            return Ok(result);
        }

[HttpPut("{id}/almacen")]
[Authorize(Roles = "Administrador")]
public async Task<IActionResult> AsignarAlmacen(int id, [FromBody] int almacenId)
{
    var user = await _userManager.FindByIdAsync(id.ToString());
    if (user is null) return NotFound();

    user.AlmacenId = almacenId;
    var update = await _userManager.UpdateAsync(user);
    if (!update.Succeeded)
        return BadRequest(update.Errors);

    return NoContent();
}

    }
}
