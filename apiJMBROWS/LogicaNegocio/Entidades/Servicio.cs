using LogicaNegocio.Entidades;
using System.ComponentModel.DataAnnotations;

public class Servicio
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MinLength(3)]
    public required string Nombre { get; set; }

    [Required]
    [MinLength(5)]
    public required string Descripcion { get; set; }

    [Required]
    [Range(5, 240)]
    public int DuracionMinutos { get; set; }

    [Required]
    [Range(0.0, 10000.0)]
    public decimal Precio { get; set; }
    [Required]
    public List<Sector> Sectores { get; set; } = new();
    // Relación solo con Habilidades (si la usás)
    public List<Habilidad> Habilidades { get; set; } = new();

    public List<ExtraServicio> Extras { get; set; } = new();


    public void EsValido()
    {
        if (string.IsNullOrWhiteSpace(Nombre) || Nombre.Length < 3)
            throw new Exception("El nombre del servicio debe tener al menos 3 caracteres.");

        if (DuracionMinutos < 5 || DuracionMinutos > 240)
            throw new Exception("La duración del servicio debe estar entre 5 y 240 minutos.");

        if (Precio < 0)
            throw new Exception("El precio no puede ser negativo.");
    }
}

