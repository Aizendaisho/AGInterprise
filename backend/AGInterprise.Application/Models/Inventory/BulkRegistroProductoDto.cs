namespace AGInterprise.Application.Models.Inventory;

public class BulkRegistroProductoDto
{
    public string Nombre { get; set; } = string.Empty;
    public string? Categoria { get; set; }
    public string? UnidadMedida { get; set; }
    public decimal PrecioUnitario { get; set; }
    public decimal CantidadInicial { get; set; }
}
