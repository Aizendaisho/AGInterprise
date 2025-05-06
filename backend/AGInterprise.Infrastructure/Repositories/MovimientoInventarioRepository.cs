using AGInterprise.Application.Interfaces;
using AGInterprise.Domain.Entities.Inventario;
using AGInterprise.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AGInterprise.Infrastructure.Repositories;

public class MovimientoInventarioRepository : IMovimientoInventarioRepository
{
    private readonly AppDbContext _context;

    public MovimientoInventarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MovimientoInventario>> ObtenerTodosAsync()
    {
        return await _context.MovimientosInventario.ToListAsync();
    }

    public async Task<MovimientoInventario?> ObtenerPorIdAsync(int id)
    {
        return await _context.MovimientosInventario.FindAsync(id);
    }

    public async Task CrearAsync(MovimientoInventario movimiento)
    {
        await _context.MovimientosInventario.AddAsync(movimiento);
        await _context.SaveChangesAsync();
    }

    public async Task ActualizarAsync(MovimientoInventario movimiento)
    {
        _context.MovimientosInventario.Update(movimiento);
        await _context.SaveChangesAsync();
    }

    public async Task EliminarAsync(int id)
    {
        var movimiento = await _context.MovimientosInventario.FindAsync(id);
        if (movimiento is not null)
        {
            _context.MovimientosInventario.Remove(movimiento);
            await _context.SaveChangesAsync();
        }
    }
}
