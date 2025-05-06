using AGInterprise.Application.Interfaces;
using AGInterprise.Domain.Entities.Inventario;
using AGInterprise.Infrastructure.Persistence;
using AGInterprise.Application.Models.Inventory;

namespace AGInterprise.Infrastructure.Services;

public class BulkProductoService : IBulkProductoService
{
    private readonly AppDbContext _context;
    private readonly IProductoRepository _prodRepo;
    private readonly IMovimientoInventarioRepository _movRepo;
    private readonly IDetalleMovimientoRepository _detMovRepo;

    public BulkProductoService(
        AppDbContext context,
        IProductoRepository prodRepo,
        IMovimientoInventarioRepository movRepo,
        IDetalleMovimientoRepository detMovRepo)
    {
        _context    = context;
        _prodRepo   = prodRepo;
        _movRepo    = movRepo;
        _detMovRepo = detMovRepo;
    }

    public async Task RegistrarProductosConStockAsync(BulkRegistroProductosRequest request)
    {
        using var tx = await _context.Database.BeginTransactionAsync();

        // 1) Crear cada producto y preparar sus detalles de entrada
        var detalles = new List<DetalleMovimiento>();
        foreach (var dto in request.Productos)
        {
            var prod = new Producto
            {
                Nombre         = dto.Nombre,
                Categoria      = dto.Categoria,
                UnidadMedida   = dto.UnidadMedida,
                PrecioUnitario = dto.PrecioUnitario
            };
            await _prodRepo.CrearAsync(prod);

            detalles.Add(new DetalleMovimiento
            {
                ProductoId = prod.Id,
                Cantidad   = dto.CantidadInicial
            });
        }

        // 2) Registrar un único movimiento de Entrada
        var mov = new MovimientoInventario
        {
            Tipo             = "Entrada",
            AlmacenDestinoId = request.AlmacenDestinoId,
            Fecha            = DateTime.UtcNow,
            Comentario       = "Carga inicial de productos"
        };
        await _movRepo.CrearAsync(mov);

        // 3) Asignar el movimiento a cada detalle y guardarlos
        foreach (var det in detalles)
        {
            det.MovimientoId = mov.Id;
            await _detMovRepo.CrearAsync(det);
        }

        await tx.CommitAsync();
    }
}
