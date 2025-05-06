// AGInterprise.Infrastructure/Repositories/InventarioRepository.cs

using AGInterprise.Application.Interfaces;
using AGInterprise.Domain.Entities.Inventario;
using AGInterprise.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AGInterprise.Infrastructure.Repositories
{
    public class InventarioRepository : IInventarioRepository
    {
        private readonly AppDbContext _context;

        public InventarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Inventario>> ObtenerTodosAsync()
            => await _context.Inventarios.ToListAsync();

        public async Task<Inventario?> ObtenerPorProductoAlmacenAsync(int productoId, int almacenId)
            => await _context.Inventarios
                .FirstOrDefaultAsync(i => i.ProductoId == productoId && i.AlmacenId == almacenId);

        public async Task ActualizarAsync(Inventario inventario)
        {
            _context.Inventarios.Update(inventario);
            await _context.SaveChangesAsync();
        }
    }
}
