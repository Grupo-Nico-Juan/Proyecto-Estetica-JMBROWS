using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LogicaAplicacion.Dtos.DtoUsuario
{
    public class RegistroAdministradorDTO
    {
        public required string TipoUsuario { get; set; } // "Cliente", "Empleado", "Administrador"
        public required string Email { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        [JsonPropertyName("password")] // <- Lo que Swagger espera
        public required string PasswordPlano { get; set; } // <- Lo que tu dominio necesita
    }
}
