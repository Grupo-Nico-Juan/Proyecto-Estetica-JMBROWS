using LogicaAplicacion.Dtos.EmpleadoDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado
{
    public interface ICUObtenerEmpleadoPorHabilidad
    {
        IEnumerable<EmpleadoDTO> Ejecutar(int habilidadId);
    }
}