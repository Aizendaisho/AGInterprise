// AGInterprise.Domain/Entities/Seguridad/Usuario.cs
using Microsoft.AspNetCore.Identity;
using AGInterprise.Domain.Entities.Almacenes;

namespace AGInterprise.Domain.Entities.Seguridad
{
    public class Usuario : IdentityUser<int>
    {
        public string Nombre { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;

        // FK opcional a Almacén para vendedores
        public int? AlmacenId { get; set; }
        public Almacen? Almacen { get; set; }
    }
}
