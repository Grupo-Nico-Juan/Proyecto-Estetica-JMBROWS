using System.Collections.Generic;
using LogicaAplicacion.Dtos.EmpleadoDTO;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado
{
    public interface ICUBuscarEmpleadosPorNombre
    {
        IEnumerable<EmpleadoDTO> Ejecutar(string texto);
    }
}