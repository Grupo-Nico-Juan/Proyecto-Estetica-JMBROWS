using System.Collections.Generic;
using LogicaAplicacion.Dtos.ServicioDTO;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUServicio
{
    public interface ICUBuscarServiciosPorNombre
    {
        IEnumerable<ServicioDTO> Ejecutar(string texto);
    }
}