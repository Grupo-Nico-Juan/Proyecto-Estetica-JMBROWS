using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class ServicioImagen
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public required string Url { get; set; }

        [Required]
        public int ServicioId { get; set; }
        [JsonIgnore]
        public Servicio Servicio { get; set; } = null!;
    }
}
