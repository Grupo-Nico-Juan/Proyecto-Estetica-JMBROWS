using LogicaAplicacion.Dtos.TurnoDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUTurno
{
    public interface ICUObtenerHorariosDisponibles
    {
        List<HorarioDisponibleDTO> Ejecutar(HorariosDisponiblesFiltroDTO filtro);
    }
}
