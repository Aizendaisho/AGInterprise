namespace AGInterprise.Domain.Entities.Inventario;

public class Inventario
{
    public int Id { get; set; }

    public int ProductoId { get; set; }

    public Producto? Producto { get; set; }

    public int AlmacenId { get; set; }

    public int? UbicacionId { get; set; }

    public decimal Cantidad { get; set; }

    public decimal StockMinimo { get; set; }

    public decimal? StockMaximo { get; set; }

    public decimal? PuntoReorden { get; set; }
}
