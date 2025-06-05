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
    public class PeriodoLaboral
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int EmpleadaId { get; set; }

        [JsonIgnore]
        public Empleado? Empleada { get; set; }

        [Required]
        public DateTime Desde { get; set; }

        [Required]
        public DateTime Hasta { get; set; }

        public string? Motivo { get; set; } // Ej: "Licencia médica", "Día libre", etc.

        public bool EsLicencia { get; set; } = false;

        public void EsValido()
        {
            if (Desde >= Hasta)
                throw new Exception("El periodo debe tener una duración válida.");

            if (Desde < DateTime.Today.AddDays(-1))
                throw new Exception("El periodo no puede comenzar en el pasado.");

            if (Motivo != null && Motivo.Length > 100)
                throw new Exception("El motivo es demasiado largo.");
        }

        public bool SeSuperpone(DateTime inicio, DateTime fin)
        {
            return !(fin <= Desde || inicio >= Hasta);
        }
    }
}
