namespace AGInterprise.WebApi.Models.Requests;

public class FacturaRequest
{
    public int ClienteId { get; set; }
    public DateTime FechaFactura { get; set; }
    public List<DetalleFacturaRequest> Detalles { get; set; } = new();
}
