public class RegistroClienteDTO
{
    public required string Nombre { get; set; }
    public required string Apellido { get; set; }
    public required string Telefono { get; set; }
    public string? Email { get; set; }
    public string? PasswordPlano { get; set; }
}