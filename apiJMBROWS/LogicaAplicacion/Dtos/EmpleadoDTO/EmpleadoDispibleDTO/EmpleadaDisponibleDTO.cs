using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.Dtos.EmpleadoDTO.EmpleadoDispibleDTO
{
    public class EmpleadaDisponibleDTO
    {
        public int Id { get; set; }
        public required string NombreCompleto { get; set; }
        public string? Color { get; set; }
        public List<int> ServiciosQuePuedeRealizar { get; set; } = [];
    }

}
