using System;
using System.Collections.Generic;
using LogicaAplicacion.Dtos.TurnoDTO;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUTurno
{
    public interface ICUObtenerTurnosDelDiaPorEmpleada
    {
        IEnumerable<TurnoDTO> Ejecutar(int empleadaId, DateTime fecha);
    }
}