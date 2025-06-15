using System.Collections.Generic;
using LogicaAplicacion.Dtos.ServicioDTO;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUServicio
{
    public interface ICUObtenerServicios
    {
        IEnumerable<ServicioDTO> Ejecutar();
    }
}