using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AGInterprise.Domain.Entities.Almacenes;
using AGInterprise.Domain.Entities.Facturacion;
using AGInterprise.Domain.Entities.Inventario;
using AGInterprise.Domain.Entities.Seguridad;  // Para ApplicationUser

namespace AGInterprise.Infrastructure.Persistence
{
    public class AppDbContext
        : IdentityDbContext<Usuario, IdentityRole<int>, int>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        // Almacenes
        public DbSet<Almacen> Almacenes => Set<Almacen>();
        public DbSet<Ubicacion> Ubicaciones => Set<Ubicacion>();

        // Inventario
        public DbSet<Producto> Productos => Set<Producto>();
        public DbSet<Inventario> Inventarios => Set<Inventario>();
        public DbSet<MovimientoInventario> MovimientosInventario => Set<MovimientoInventario>();
        public DbSet<DetalleMovimiento> DetallesMovimiento => Set<DetalleMovimiento>();

        // Facturación
        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<Factura> Facturas => Set<Factura>();
        public DbSet<DetalleFactura> DetallesFactura => Set<DetalleFactura>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configuración de la relación entre ApplicationUser y Almacen
            builder.Entity<Usuario>()
                .HasOne(u => u.Almacen)
                .WithMany()
                .HasForeignKey(u => u.AlmacenId)
                .OnDelete(DeleteBehavior.SetNull);

            // Opcional: renombrar tablas de Identity (si se desea)
            // builder.Entity<ApplicationUser>().ToTable("Usuarios");
            // builder.Entity<IdentityRole<int>>().ToTable("Roles");
            // builder.Entity<IdentityUserRole<int>>().ToTable("UsuarioRoles");
        }
    }
}
