namespace AGInterprise.Domain.Entities.Inventario;

public class MovimientoInventario
{
    public int Id { get; set; }

    public string Tipo { get; set; } = "Entrada"; // Entrada, Salida, Transferencia

    public int? AlmacenOrigenId { get; set; }

    public int? AlmacenDestinoId { get; set; }

    public DateTime Fecha { get; set; } = DateTime.UtcNow;

    public string? Comentario { get; set; }

    public ICollection<DetalleMovimiento> Detalles { get; set; } = new List<DetalleMovimiento>();
}
