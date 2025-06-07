using System.Collections.Generic;
using LogicaAplicacion.Dtos.HabilidadDTO;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado
{
    public interface ICUObtenerHabilidadesDeEmpleado
    {
        IEnumerable<HabilidadDTO> Ejecutar(int empleadoId);
    }
}