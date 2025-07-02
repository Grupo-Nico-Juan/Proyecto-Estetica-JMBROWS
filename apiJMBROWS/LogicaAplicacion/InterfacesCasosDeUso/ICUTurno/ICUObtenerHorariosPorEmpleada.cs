using LogicaAplicacion.Dtos.TurnoDTO;
using System.Collections.Generic;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUTurno
{
    public interface ICUObtenerHorariosPorEmpleada
    {
        List<HorarioDisponibleDTO> Ejecutar(HorariosPorEmpleadaFiltroDTO filtro);
    }
}
