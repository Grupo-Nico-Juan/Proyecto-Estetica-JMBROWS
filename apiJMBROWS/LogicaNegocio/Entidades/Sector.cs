using LogicaNegocio.Entidades;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Sector
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MinLength(3)]
    public required string Nombre { get; set; }

    [Required]
    public int SucursalId { get; set; }

    [JsonIgnore]
    public Sucursal? Sucursal { get; set; }

    public List<Servicio> Servicios { get; set; } = new();

    public void EsValido()
    {
        if (string.IsNullOrWhiteSpace(Nombre) || Nombre.Length < 3)
            throw new Exception("El nombre del sector debe tener al menos 3 caracteres.");
    }
}
