using System.Collections.Generic;
using LogicaAplicacion.Dtos.SectorDTO;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado
{
    public interface ICUObtenerSectoresDeEmpleado
    {
        IEnumerable<SectorDTSSuc> Ejecutar(int empleadoId);
    }
}