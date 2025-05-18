using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Dtos.DtoUsuario
{
    public class RegistroUsuarioDTO
    {
        public required string TipoUsuario { get; set; } // "Cliente", "Empleado", "Administrador"
        public required string Email { get; set; }
        public required string Nombre { get; set; }
        public required string Apellido { get; set; }
        public required string Password { get; set; }
    }
}
