using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class DetalleTurno
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TurnoId { get; set; }

        [JsonIgnore]
        public Turno? Turno { get; set; }

        [Required]
        public int ServicioId { get; set; }

        public Servicio? Servicio { get; set; }

        public List<ExtraServicio> Extras { get; set; } = new();

        [NotMapped]
        public DateTime HoraInicio { get; set; }
        [NotMapped]
        public DateTime HoraFin { get; set; }

        public void EsValido()
        {
            if (Servicio == null)
                throw new Exception("El servicio es obligatorio en el detalle del turno.");

            Servicio.EsValido();
        }
    }
}
