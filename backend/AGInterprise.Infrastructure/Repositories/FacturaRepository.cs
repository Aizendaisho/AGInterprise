using AGInterprise.Application.Interfaces;
using AGInterprise.Domain.Entities.Facturacion;
using AGInterprise.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AGInterprise.Infrastructure.Repositories
{
    public class FacturaRepository : IFacturaRepository
    {
        private readonly AppDbContext _context;
        public FacturaRepository(AppDbContext context)
            => _context = context;

        public async Task<IEnumerable<Factura>> ObtenerTodasAsync()
            => await _context.Facturas
                             .Include(f => f.Detalles)
                             .ToListAsync();

        public async Task<Factura?> ObtenerPorIdAsync(int id)
            => await _context.Facturas
                             .Include(f => f.Detalles)
                             .FirstOrDefaultAsync(f => f.Id == id);

        public async Task CrearAsync(Factura factura)
        {
            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Factura factura)
        {
            _context.Facturas.Update(factura);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(int id)
        {
            var f = await _context.Facturas.FindAsync(id);
            if (f != null)
            {
                _context.Facturas.Remove(f);
                await _context.SaveChangesAsync();
            }
        }
    }
}
