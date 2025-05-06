using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using AGInterprise.Domain.Entities.Almacenes;
using AGInterprise.Domain.Entities.Facturacion;
using AGInterprise.Domain.Entities.Inventario;
using AGInterprise.Domain.Entities.Seguridad;


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

        // NOTA: Ya no necesitas un DbSet<Usuario> aquí,
        // IdentityDbContext ya expone Users, Roles, UserRoles, etc.

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Si quieres renombrar las tablas de Identity, por ejemplo:
            // builder.Entity<Usuario>().ToTable("Usuarios");
            // builder.Entity<IdentityRole<int>>().ToTable("Roles");
            // builder.Entity<IdentityUserRole<int>>().ToTable("UsuarioRoles");
            // builder.Entity<IdentityUserClaim<int>>().ToTable("UsuarioClaims");
            // builder.Entity<IdentityUserLogin<int>>().ToTable("UsuarioLogins");
            // builder.Entity<IdentityRoleClaim<int>>().ToTable("RolClaims");
            // builder.Entity<IdentityUserToken<int>>().ToTable("UsuarioTokens");

            // Aquí puedes agregar configuraciones adicionales de Fluent API
            // para tus entidades de dominio (p. ej. índices, relaciones, etc.).
        }
    }
}
