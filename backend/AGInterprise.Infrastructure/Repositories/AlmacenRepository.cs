using AGInterprise.Application.Interfaces;
using AGInterprise.Domain.Entities.Almacenes;
using AGInterprise.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AGInterprise.Infrastructure.Repositories;

public class AlmacenRepository : IAlmacenRepository
{
    private readonly AppDbContext _context;

    public AlmacenRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Almacen>> ObtenerTodosAsync()
    {
        return await _context.Almacenes.ToListAsync();
    }

    public async Task<Almacen?> ObtenerPorIdAsync(int id)
    {
        return await _context.Almacenes.FindAsync(id);
    }

public async Task CrearAsync(Almacen almacen)
{
    // ¿Hay algún almacen predeterminado?
    bool existePredeterminado = await _context.Almacenes.AnyAsync(a => a.EsPredeterminado);
    if (!existePredeterminado)
    {
        // Este será el primero => predeterminado
        almacen.EsPredeterminado = true;
    }

    await _context.Almacenes.AddAsync(almacen);
    await _context.SaveChangesAsync();
}

    public async Task ActualizarAsync(Almacen almacen)
    {
        _context.Almacenes.Update(almacen);
        await _context.SaveChangesAsync();
    }

    public async Task EliminarAsync(int id)
    {
        var almacen = await _context.Almacenes.FindAsync(id);
        if (almacen is not null)
        {
            _context.Almacenes.Remove(almacen);
            await _context.SaveChangesAsync();
        }
    }

        public async Task<Almacen?> ObtenerPredeterminadoAsync()
    {
        return await _context.Almacenes.FirstOrDefaultAsync(a => a.EsPredeterminado);
    }

}
