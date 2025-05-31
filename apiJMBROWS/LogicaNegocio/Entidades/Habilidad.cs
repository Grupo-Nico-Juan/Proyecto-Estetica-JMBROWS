using Libreria.LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LogicaNegocio.Entidades
{
    public class Habilidad
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public required string Nombre { get; set; }

        [Required]
        [MinLength(5)]
        public required string Descripcion { get; set; }

        // Relaciones
        public List<Empleado> Empleadas { get; set; } = new();
        public List<Servicio> Servicios { get; set; } = new();

        [JsonIgnore]
        public bool Eliminada { get; set; } = false;

        public void EsValido()
        {
            if (string.IsNullOrWhiteSpace(Nombre) || Nombre.Length < 3)
                throw new Exception("El nombre de la habilidad debe tener al menos 3 caracteres.");

            if (string.IsNullOrWhiteSpace(Descripcion) || Descripcion.Length < 5)
                throw new Exception("La descripción debe tener al menos 5 caracteres.");
        }
    }
}
