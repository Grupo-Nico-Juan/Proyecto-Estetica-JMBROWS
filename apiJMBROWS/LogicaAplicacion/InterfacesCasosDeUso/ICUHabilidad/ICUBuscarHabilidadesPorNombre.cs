using System.Collections.Generic;
using LogicaAplicacion.Dtos.HabilidadDTO;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUHabilidad
{
    public interface ICUBuscarHabilidadesPorNombre
    {
        IEnumerable<ActualizarHabilidadDTO> Ejecutar(string texto);
    }
}