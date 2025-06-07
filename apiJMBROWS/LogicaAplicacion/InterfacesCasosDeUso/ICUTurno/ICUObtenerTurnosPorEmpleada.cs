using System.Collections.Generic;
using LogicaAplicacion.Dtos.TurnoDTO;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUTurno
{
    public interface ICUObtenerTurnosPorEmpleada
    {
        IEnumerable<TurnoDTO> Ejecutar(int empleadaId);
    }
}