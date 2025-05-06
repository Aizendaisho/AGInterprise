using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace AGInterprise.Infrastructure.Persistence
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // 1) Carga appsettings.json del proyecto WebApi
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../AGInterprise.WebApi"))
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            // 2) Construye opciones con la cadena de conexión
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            var connStr = config.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connStr);

            // 3) Devuelve el contexto
            return new AppDbContext(builder.Options);
        }
    }
}
