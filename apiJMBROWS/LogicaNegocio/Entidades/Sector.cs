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
    public class Sector
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public required string Nombre { get; set; }

        // Relaciones

        [Required]
        public int SucursalId { get; set; }

        [JsonIgnore]
        public Sucursal? Sucursal { get; set; }

        public List<Servicio> Servicios { get; set; } = new();
        public List<Empleado> Empleadas { get; set; } = new();

        [JsonIgnore]
        public bool Eliminado { get; set; } = false;

        public void EsValido()
        {
            if (string.IsNullOrWhiteSpace(Nombre) || Nombre.Length < 3)
                throw new Exception("El nombre del sector debe tener al menos 3 caracteres.");
        }
    }
}
