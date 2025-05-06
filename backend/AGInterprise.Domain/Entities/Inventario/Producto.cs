namespace AGInterprise.Domain.Entities.Inventario;

public class Producto
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string? CodigoBarras { get; set; }

    public string? Categoria { get; set; }

    public string? UnidadMedida { get; set; }

    public decimal PrecioUnitario { get; set; }

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    public DateTime? FechaActualizacion { get; set; }

    public ICollection<Inventario> Inventarios { get; set; } = new List<Inventario>();

    public ICollection<DetalleMovimiento> Movimientos { get; set; } = new List<DetalleMovimiento>();
}
