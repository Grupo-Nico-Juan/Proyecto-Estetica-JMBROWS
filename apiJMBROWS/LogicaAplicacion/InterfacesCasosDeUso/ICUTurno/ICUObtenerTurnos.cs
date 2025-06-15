using System.Collections.Generic;
using LogicaAplicacion.Dtos.TurnoDTO;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUTurno
{
    public interface ICUObtenerTurnos
    {
        IEnumerable<TurnoDTO> Ejecutar();
    }
}