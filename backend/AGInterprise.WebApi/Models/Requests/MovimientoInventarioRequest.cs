namespace AGInterprise.WebApi.Models.Requests;

public class MovimientoInventarioRequest
{
    public string Tipo { get; set; } = default!;           // Entrada, Salida, Transferencia
    public int? AlmacenOrigenId { get; set; }
    public int? AlmacenDestinoId { get; set; }
    public string? Comentario { get; set; }
    public List<DetalleMovimientoRequest> Detalles { get; set; } = new();
}
