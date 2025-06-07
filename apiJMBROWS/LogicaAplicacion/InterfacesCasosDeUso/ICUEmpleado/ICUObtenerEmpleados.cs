using System.Collections.Generic;
using LogicaAplicacion.Dtos.EmpleadoDTO;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado
{
    public interface ICUObtenerEmpleados
    {
        IEnumerable<EmpleadoDTO> Ejecutar();
    }
}