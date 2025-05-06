// AGInterprise.WebApi/Controllers/InventarioController.cs

using AGInterprise.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AGInterprise.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventarioController : ControllerBase
    {
        private readonly IInventarioRepository _invRepo;
        public InventarioController(IInventarioRepository invRepo)
            => _invRepo = invRepo;

        // GET api/inventario
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
            => Ok(await _invRepo.ObtenerTodosAsync());

        // GET api/inventario/{productoId}/{almacenId}
        [HttpGet("{productoId:int}/{almacenId:int}")]
        [Authorize]
        public async Task<IActionResult> GetByProductoAlmacen(int productoId, int almacenId)
        {
            var inv = await _invRepo.ObtenerPorProductoAlmacenAsync(productoId, almacenId);
            return inv is null ? NotFound() : Ok(inv);
        }
    }
}
