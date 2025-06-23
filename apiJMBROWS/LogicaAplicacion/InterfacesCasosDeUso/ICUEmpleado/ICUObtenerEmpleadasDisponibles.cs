using LogicaAplicacion.Dtos.EmpleadoDTO.EmpleadoDispibleDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado
{
    public interface ICUObtenerEmpleadasDisponibles
    {
        public List<EmpleadaDisponibleDTO> Ejecutar(ConsultaEmpleadasDisponiblesDTO dto);
    }
}
