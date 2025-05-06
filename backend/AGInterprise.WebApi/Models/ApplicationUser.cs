using AGInterprise.Domain.Entities.Almacenes;
using Microsoft.AspNetCore.Identity;

namespace AGInterprise.WebApi.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        // FK al almacén: sólo para vendedores
        public int? AlmacenId { get; set; }

        // Navegación (opcional)
        public Almacen? Almacen { get; set; }
    }
}
