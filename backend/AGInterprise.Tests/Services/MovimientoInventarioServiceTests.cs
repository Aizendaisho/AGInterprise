using System;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.Data.Sqlite;               // <<< para el proveedor SQLite
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AGInterprise.Infrastructure.Services;
using AGInterprise.Application.Interfaces;
using AGInterprise.Domain.Entities.Inventario;
using AGInterprise.Infrastructure.Persistence;

namespace AGInterprise.Tests.Services
{
    public class MovimientoInventarioServiceTests
    {
        [Fact]
public async Task ProcesarMovimiento_Throws_When_StockInsuficiente()
{
    // 1) Creamos una conexión SQLite en memoria y la dejamos abierta
    var connection = new SqliteConnection("DataSource=:memory:");
    await connection.OpenAsync();

    // 2) Configuramos el DbContext para usar esa conexión
    var options = new DbContextOptionsBuilder<AppDbContext>()
        .UseSqlite(connection)
        .Options;

    // 3) Inicializamos el schema en la BD in-memory
    await using var dbContext = new AppDbContext(options);
    await dbContext.Database.EnsureCreatedAsync();

    // 4) Mock de repos
    var invRepo = new Mock<IInventarioRepository>();
    invRepo
      .Setup(r => r.ObtenerPorProductoAlmacenAsync(1, 1))
      .ReturnsAsync((Inventario?)null);

    var movRepo = new Mock<IMovimientoInventarioRepository>();
    var detMov  = new Mock<IDetalleMovimientoRepository>();

    // 5) Creamos el servicio con la conexión SQLite
    var service = new MovimientoInventarioService(
        dbContext,
        invRepo.Object,
        movRepo.Object,
        detMov.Object
    );

    // 6) Preparamos el movimiento
    var mov = new MovimientoInventario
    {
        Tipo            = "Salida",
        AlmacenOrigenId = 1,
        Detalles        = { new DetalleMovimiento { ProductoId = 1, Cantidad = 5 } }
    };

    // 7) Ahora sí llegará a tu validación de stock
    await Assert.ThrowsAsync<Exception>(() =>
        service.ProcesarMovimientoAsync(mov)
    );
}

    }
}
