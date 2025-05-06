using System.ComponentModel.DataAnnotations.Schema;
using AGInterprise.Domain.Entities.Almacenes;
using Microsoft.AspNetCore.Identity;

namespace AGInterprise.Domain.Entities.Seguridad
{
    public class Usuario : IdentityUser<int>
    {
        // Para mostrar un nombre "amigable" o completo:
        public string NombreCompleto { get; set; } = string.Empty;

        // Si quieres conservar tu campo "Activo":
        public bool Activo { get; set; } = true;

        // Si vas a asignar almacén a vendedores:
        public int? AlmacenId { get; set; }

        [ForeignKey(nameof(AlmacenId))]
        public Almacen? Almacen { get; set; }

        // Ya no necesitas:
        // public string Correo { get; set; }
        // public string ContrasenaHash { get; set; }
        // public string Rol { get; set; }
        //
        // porque:
        // - Correo → IdentityUser.Email
        // - ContrasenaHash → IdentityUser.PasswordHash
        // - Rol → ASP NET Identity maneja roles en su tabla AspNetUserRoles
    }
}
