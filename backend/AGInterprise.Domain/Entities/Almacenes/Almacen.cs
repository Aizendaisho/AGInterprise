namespace AGInterprise.Domain.Entities.Almacenes;

public class Almacen
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string? Descripcion { get; set; }

    public string? Direccion { get; set; }

    public bool Activo { get; set; } = true;

    public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;

    public DateTime? FechaActualizacion { get; set; }
    public bool EsPredeterminado { get; set; } = false;


    public ICollection<Ubicacion> Ubicaciones { get; set; } = new List<Ubicacion>();
}
