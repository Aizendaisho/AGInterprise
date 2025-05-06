namespace AGInterprise.Domain.Entities.Facturacion;

public class DetalleFactura
{
    public int Id { get; set; }

    public int FacturaId { get; set; }

    public Factura? Factura { get; set; }

    public int ProductoId { get; set; }

    public decimal Cantidad { get; set; }

    public decimal PrecioUnitario { get; set; }

    public decimal Total => Cantidad * PrecioUnitario;
}
