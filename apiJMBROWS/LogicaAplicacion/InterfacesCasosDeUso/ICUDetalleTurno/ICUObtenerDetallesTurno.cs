using System.Collections.Generic;
using LogicaAplicacion.Dtos.TurnoDTO;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUDetalleTurno
{
    public interface ICUObtenerDetallesTurno
    {
        IEnumerable<DetalleTurnoDTO> Ejecutar();
    }
}