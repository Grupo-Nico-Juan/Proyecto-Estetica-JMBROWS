using LogicaAplicacion.Dtos.HabilidadDTO;
using LogicaAplicacion.Dtos.ServicioDTO;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.InterfacesCasosDeUso.ICUServicio
{
    public interface ICUObtenerHabilidadesServicio
    {
        List<ServicioHabilidadDTO> Ejecutar(int servicioId);
    }
}
