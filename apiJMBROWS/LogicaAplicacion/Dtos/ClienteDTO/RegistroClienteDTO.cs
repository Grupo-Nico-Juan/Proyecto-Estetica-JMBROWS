using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LogicaAplicacion.Dtos.ClienteDTO
{
    public class RegistroClienteDTO
    {
        [Required]
        public required string Nombre { get; set; }

        [Required]
        public required string Apellido { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required string Telefono { get; set; }

        [JsonPropertyName("password")] // <- Lo que Swagger espera
        public required string PasswordPlano { get; set; }
    }

}
