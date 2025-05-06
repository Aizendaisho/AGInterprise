namespace AGInterprise.WebApi.Models.Requests;

public class DetalleFacturaRequest
{
    public int ProductoId { get; set; }
    public decimal Cantidad { get; set; }
    public decimal PrecioUnitario { get; set; }
}
