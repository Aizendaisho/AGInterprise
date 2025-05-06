namespace AGInterprise.Domain.Entities.Facturacion;

public class Factura
{
    public int Id { get; set; }

    public int ClienteId { get; set; }

    public Cliente? Cliente { get; set; }

    public int UsuarioId { get; set; }

    public DateTime Fecha { get; set; } = DateTime.UtcNow;
    public DateTime FechaFactura { get; set; } = DateTime.UtcNow;


    public string? TipoComprobante { get; set; }

    public string? NCF { get; set; }

    public decimal Total { get; set; }

    public string Estado { get; set; } = "Emitida";

    public ICollection<DetalleFactura> Detalles { get; set; } = new List<DetalleFactura>();
}
