namespace AGInterprise.Domain.Entities.Inventario;

public class DetalleMovimiento
{
    public int Id { get; set; }

    public int MovimientoId { get; set; }

    public MovimientoInventario? Movimiento { get; set; }

    public int ProductoId { get; set; }

    public Producto? Producto { get; set; }

    public decimal Cantidad { get; set; }
}
