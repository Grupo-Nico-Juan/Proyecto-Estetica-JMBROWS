using System.ComponentModel.DataAnnotations;

namespace LogicaNegocio.Entidades
{
    public class ExtraServicio
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public required string Nombre { get; set; }

        [Range(1, 240)]
        public int DuracionMinutos { get; set; }

        [Range(0.0, 10000.0)]
        public decimal Precio { get; set; }

        public int ServicioId { get; set; }
        public Servicio? Servicio { get; set; }

        public void EsValido()
        {
            if (string.IsNullOrWhiteSpace(Nombre) || Nombre.Length < 3)
                throw new Exception("El nombre del extra debe tener al menos 3 caracteres.");
            if (DuracionMinutos <= 0)
                throw new Exception("La duracion debe ser positiva.");
            if (Precio < 0)
                throw new Exception("El precio no puede ser negativo.");
        }
    }
}
