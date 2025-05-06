using AGInterprise.Application.Interfaces;
using AGInterprise.Domain.Entities.Inventario;
using AGInterprise.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AGInterprise.Infrastructure.Repositories;

public class DetalleMovimientoRepository : IDetalleMovimientoRepository
{
    private readonly AppDbContext _context;

    public DetalleMovimientoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<DetalleMovimiento>> ObtenerTodosAsync()
    {
        return await _context.DetallesMovimiento.ToListAsync();
    }

    public async Task<DetalleMovimiento?> ObtenerPorIdAsync(int id)
    {
        return await _context.DetallesMovimiento.FindAsync(id);
    }

    public async Task CrearAsync(DetalleMovimiento detalle)
    {
        await _context.DetallesMovimiento.AddAsync(detalle);
        await _context.SaveChangesAsync();
    }

    public async Task ActualizarAsync(DetalleMovimiento detalle)
    {
        _context.DetallesMovimiento.Update(detalle);
        await _context.SaveChangesAsync();
    }

    public async Task EliminarAsync(int id)
    {
        var detalle = await _context.DetallesMovimiento.FindAsync(id);
        if (detalle is not null)
        {
            _context.DetallesMovimiento.Remove(detalle);
            await _context.SaveChangesAsync();
        }
    }
}
