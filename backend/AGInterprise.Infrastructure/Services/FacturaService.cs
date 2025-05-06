using System;
using System.Linq;
using System.Threading.Tasks;
using AGInterprise.Application.Interfaces;
using AGInterprise.Domain.Entities.Facturacion;
using AGInterprise.Domain.Entities.Inventario;
using AGInterprise.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AGInterprise.Infrastructure.Services
{
    public class FacturaService : IFacturaService
    {
        private readonly AppDbContext _context;
        private readonly IAlmacenRepository _almRepo;
        private readonly IInventarioRepository _invRepo;
        private readonly IFacturaRepository _factRepo;
        private readonly IDetalleFacturaRepository _detFactRepo;
        private readonly IMovimientoInventarioService _movService;

        public FacturaService(
            AppDbContext context,
            IAlmacenRepository almRepo,
            IInventarioRepository invRepo,
            IFacturaRepository factRepo,
            IDetalleFacturaRepository detFactRepo,
            IMovimientoInventarioService movService)
        {
            _context     = context;
            _almRepo     = almRepo;
            _invRepo     = invRepo;
            _factRepo    = factRepo;
            _detFactRepo = detFactRepo;
            _movService  = movService;
        }

        public async Task<Factura> CrearFacturaCompletaAsync(Factura factura, int usuarioId)
        {
            // 0. Obtener almacén predeterminado
            var almacen = await _almRepo.ObtenerPredeterminadoAsync()
                ?? throw new Exception("No hay almacén predeterminado configurado.");

            // 1. Asignar usuario y calcular total
            factura.UsuarioId = usuarioId;
            factura.Total     = factura.Detalles.Sum(d => d.Cantidad * d.PrecioUnitario);

            // 2. Iniciar transacción
            await using var tx = await _context.Database.BeginTransactionAsync();

            // 3. Validar que hay stock suficiente y descontarlo del inventario
            foreach (var det in factura.Detalles)
            {
                var inv = await _invRepo.ObtenerPorProductoAlmacenAsync(det.ProductoId, almacen.Id);
                if (inv == null || inv.Cantidad < det.Cantidad)
                    throw new Exception($"Stock insuficiente para producto {det.ProductoId}.");

                inv.Cantidad -= det.Cantidad;
                await _invRepo.ActualizarAsync(inv);
            }

            // 4. Guardar factura y sus detalles de golpe
            _context.Facturas.Add(factura);
            await _context.SaveChangesAsync();

            // 5. Crear y procesar movimiento de salida (stock)
            var movSalida = new MovimientoInventario
            {
                Tipo            = "Salida",
                AlmacenOrigenId = almacen.Id,
                Fecha           = DateTime.UtcNow,
                Comentario      = $"Salida por factura {factura.Id}",
                Detalles        = factura.Detalles
                    .Select(d => new DetalleMovimiento
                    {
                        ProductoId = d.ProductoId,
                        Cantidad   = d.Cantidad
                    })
                    .ToList()
            };
            await _movService.ProcesarMovimientoAsync(movSalida);

            // 6. Confirmar transacción
            await tx.CommitAsync();

            return factura;
        }
    }
}
