using System;
using System.Linq;
using System.Threading.Tasks;
using AGInterprise.Application.Interfaces;
using AGInterprise.Domain.Entities.Inventario;
using AGInterprise.Infrastructure.Persistence;

namespace AGInterprise.Infrastructure.Services
{
    public class MovimientoInventarioService : IMovimientoInventarioService
    {
        private readonly AppDbContext _context;
        private readonly IInventarioRepository _invRepo;
        private readonly IMovimientoInventarioRepository _movRepo;
        private readonly IDetalleMovimientoRepository _detMovRepo;

        public MovimientoInventarioService(
            AppDbContext context,
            IInventarioRepository invRepo,
            IMovimientoInventarioRepository movRepo,
            IDetalleMovimientoRepository detMovRepo)
        {
            _context    = context;
            _invRepo    = invRepo;
            _movRepo    = movRepo;
            _detMovRepo = detMovRepo;
        }

        public async Task<MovimientoInventario> ProcesarMovimientoAsync(MovimientoInventario movimiento)
        {
            // Solo abrimos transacción si no hay una ya activa
            var tx = _context.Database.CurrentTransaction is null
                ? await _context.Database.BeginTransactionAsync()
                : null;

            try
            {
                await ProcesarMovimientoInternoAsync(movimiento);

                if (tx is not null)
                    await tx.CommitAsync();

                // Devolver el movimiento recargado con sus detalles
                return await _movRepo.ObtenerPorIdAsync(movimiento.Id)
                       ?? movimiento;
            }
            catch
            {
                if (tx is not null)
                    await tx.RollbackAsync();
                throw;
            }
        }

        private async Task ProcesarMovimientoInternoAsync(MovimientoInventario movimiento)
        {
            // Extraemos y vaciamos los detalles para evitar cascada
            var detalles = movimiento.Detalles.ToList();
            movimiento.Detalles.Clear();

            // 1) Crear solo el encabezado del movimiento
            await _movRepo.CrearAsync(movimiento);

            // 2) Procesar cada detalle y ajustar inventario
            foreach (var det in detalles)
            {
                // Insertar el detalle
                det.MovimientoId = movimiento.Id;
                await _detMovRepo.CrearAsync(det);

                // Determinar almacén según tipo
                int almacenId = movimiento.Tipo switch
                {
                    "Entrada"      => movimiento.AlmacenDestinoId
                                        ?? throw new Exception("Falta almacenDestinoId"),
                    "Salida"       => movimiento.AlmacenOrigenId
                                        ?? throw new Exception("Falta almacenOrigenId"),
                    "Transferencia"=> movimiento.AlmacenOrigenId
                                        ?? throw new Exception("Falta almacenOrigenId"),
                    _              => throw new Exception("Tipo de movimiento inválido")
                };

                // Ajustar inventario
                var inv = await _invRepo.ObtenerPorProductoAlmacenAsync(det.ProductoId, almacenId);
                switch (movimiento.Tipo)
                {
                    case "Entrada":
                        if (inv is null)
                        {
                            inv = new Inventario
                            {
                                ProductoId  = det.ProductoId,
                                AlmacenId   = almacenId,
                                Cantidad    = det.Cantidad,
                                StockMinimo = 0
                            };
                            _context.Inventarios.Add(inv);
                            // Persistir el nuevo inventario
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            inv.Cantidad += det.Cantidad;
                            await _invRepo.ActualizarAsync(inv);
                        }
                        break;

                    case "Salida":
                        if (inv is null || inv.Cantidad < det.Cantidad)
                            throw new Exception($"Stock insuficiente para producto {det.ProductoId} en almacén {almacenId}.");
                        inv.Cantidad -= det.Cantidad;
                        await _invRepo.ActualizarAsync(inv);
                        break;

                    case "Transferencia":
                        // Resta en origen
                        if (inv is null || inv.Cantidad < det.Cantidad)
                            throw new Exception($"Stock insuficiente para producto {det.ProductoId} en almacén {almacenId}.");
                        inv.Cantidad -= det.Cantidad;
                        await _invRepo.ActualizarAsync(inv);

                        // Suma en destino
                        int destId = movimiento.AlmacenDestinoId
                                     ?? throw new Exception("Falta almacenDestinoId");
                        var invDest = await _invRepo.ObtenerPorProductoAlmacenAsync(det.ProductoId, destId);
                        if (invDest is null)
                        {
                            invDest = new Inventario
                            {
                                ProductoId  = det.ProductoId,
                                AlmacenId   = destId,
                                Cantidad    = det.Cantidad,
                                StockMinimo = 0
                            };
                            _context.Inventarios.Add(invDest);
                            // Persistir el nuevo inventario del destino
                            await _context.SaveChangesAsync();
                        }
                        else
                        {
                            invDest.Cantidad += det.Cantidad;
                            await _invRepo.ActualizarAsync(invDest);
                        }
                        break;
                }
            }
        }
    }
}
