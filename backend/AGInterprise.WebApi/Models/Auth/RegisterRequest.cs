public class RegisterRequest
{
    public string Username       { get; set; } = string.Empty;
    public string Email          { get; set; } = string.Empty;
    public string Password       { get; set; } = string.Empty;
    public string NombreCompleto { get; set; } = string.Empty;
    public string Role           { get; set; } = string.Empty;  // Administrador/Supervisor/Vendedor
    public int? AlmacenId        { get; set; }  // Obligatorio si Role=="Vendedor"
}