// WebApi/Models/UsuarioWithFacturasDto.cs
using System.Collections.Generic;
using AGInterprise.Domain.Entities.Facturacion;

namespace AGInterprise.WebApi.Models
{
    public class UsuarioWithFacturasDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; }    = string.Empty;
        public IList<string> Roles { get; set; } = new List<string>();
        public IEnumerable<Factura> Facturas { get; set; } = new List<Factura>();
    }
}
