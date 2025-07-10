using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class Notificacion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public required string Destinatario { get; set; } // Email o teléfono

        [Required]
        public required string Medio { get; set; } // Ej: "WhatsApp", "Email"

        [Required]
        public required string Mensaje { get; set; }

        public DateTimeOffset FechaEnvio { get; set; } = DateTime.UtcNow;

        public bool Enviada { get; set; } = false;

        public int? TurnoId { get; set; }
        [JsonIgnore]
        public Turno? Turno { get; set; }

        public int? PromocionId { get; set; }
        [JsonIgnore]
        public Promocion? Promocion { get; set; }

        public void EsValido()
        {
            if (string.IsNullOrWhiteSpace(Destinatario))
                throw new Exception("La notificación debe tener un destinatario válido.");

            if (string.IsNullOrWhiteSpace(Medio) || (Medio != "WhatsApp" && Medio != "Email"))
                throw new Exception("El medio debe ser 'WhatsApp' o 'Email'.");

            if (string.IsNullOrWhiteSpace(Mensaje) || Mensaje.Length < 5)
                throw new Exception("El mensaje de la notificación es demasiado corto.");
        }
    }
}
