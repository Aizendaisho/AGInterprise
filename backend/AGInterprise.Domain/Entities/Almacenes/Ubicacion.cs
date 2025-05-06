namespace AGInterprise.Domain.Entities.Almacenes;

public class Ubicacion
{
    public int Id { get; set; }

    public int AlmacenId { get; set; }

    public Almacen? Almacen { get; set; }

    public string Codigo { get; set; } = string.Empty;

    public string? Descripcion { get; set; }
}
