using System.Collections.Generic;
using LogicaAplicacion.Dtos.HabilidadDTO;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUHabilidad
{
    public interface ICUObtenerHabilidades
    {
        IEnumerable<HabilidadDTO> Ejecutar();
    }
}