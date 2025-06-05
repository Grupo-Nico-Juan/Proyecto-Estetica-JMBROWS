using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        [Range(1, 300)]
        public int DuracionMinutos { get; set; }

        [Required]
        [Range(0.0, 10000.0)]
        public decimal Precio { get; set; }

        public void EsValido()
        {
            if (DuracionMinutos <= 0 || DuracionMinutos > 300)
                throw new Exception("Duración inválida en detalle del turno.");

            if (Precio < 0)
                throw new Exception("Precio inválido en detalle del turno.");
        }
    }
}
