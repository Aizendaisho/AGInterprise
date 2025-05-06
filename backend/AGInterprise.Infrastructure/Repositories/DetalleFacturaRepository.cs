using AGInterprise.Application.Interfaces;
using AGInterprise.Domain.Entities.Facturacion;
using AGInterprise.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AGInterprise.Infrastructure.Repositories;

public class DetalleFacturaRepository : IDetalleFacturaRepository
{
    private readonly AppDbContext _context;

    public DetalleFacturaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DetalleFactura>> ObtenerTodosAsync()
    {
        return await _context.DetallesFactura.ToListAsync();
    }

    public async Task<DetalleFactura?> ObtenerPorIdAsync(int id)
    {
        return await _context.DetallesFactura.FindAsync(id);
    }

    public async Task CrearAsync(DetalleFactura detalle)
    {
        await _context.DetallesFactura.AddAsync(detalle);
        await _context.SaveChangesAsync();
    }

    public async Task ActualizarAsync(DetalleFactura detalle)
    {
        _context.DetallesFactura.Update(detalle);
        await _context.SaveChangesAsync();
    }

    public async Task EliminarAsync(int id)
    {
        var detalle = await _context.DetallesFactura.FindAsync(id);
        if (detalle is not null)
        {
            _context.DetallesFactura.Remove(detalle);
            await _context.SaveChangesAsync();
        }
    }
}
