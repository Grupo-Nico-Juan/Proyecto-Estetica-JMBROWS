using LogicaAplicacion.Dtos.TurnoDTO;
using System.Collections.Generic;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUTurno
{
    public interface ICUObtenerHorariosOcupados
    {
        List<HorarioOcupadoDTO> Ejecutar(HorariosDisponiblesFiltroDTO filtro);
    }
}
