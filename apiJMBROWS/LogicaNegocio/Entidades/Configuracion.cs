using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class Configuracion
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public required string Clave { get; set; }

        [Required]
        public required string Valor { get; set; }

        public string? Descripcion { get; set; }

        public void EsValido()
        {
            if (string.IsNullOrWhiteSpace(Clave))
                throw new Exception("La clave de configuración no puede estar vacía.");

            if (string.IsNullOrWhiteSpace(Valor))
                throw new Exception("El valor de configuración no puede estar vacío.");
        }
    }
}
