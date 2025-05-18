using LogicaNegocio.Entidades;
using System.ComponentModel.DataAnnotations;

namespace Libreria.LogicaNegocio.Entidades
{
    public class Empleado : Usuario
    {
        [Required]
        public required string Cargo { get; set; }

        public override string Rol => "Empleado";
    }
}

