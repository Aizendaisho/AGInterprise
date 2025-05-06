using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

// Añade esto:
using AGInterprise.Domain.Entities.Almacenes;

namespace AGInterprise.WebApi.Models
{
    public class ApplicationUser : IdentityUser<int>
    {
        public int? AlmacenId { get; set; }

        [ForeignKey(nameof(AlmacenId))]
        public Almacen? Almacen { get; set; }
    }
}
