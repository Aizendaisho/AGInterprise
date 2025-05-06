namespace AGInterprise.Domain.Entities.Facturacion;

public class Cliente
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string? RNC { get; set; }

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public ICollection<Factura> Facturas { get; set; } = new List<Factura>();
}
