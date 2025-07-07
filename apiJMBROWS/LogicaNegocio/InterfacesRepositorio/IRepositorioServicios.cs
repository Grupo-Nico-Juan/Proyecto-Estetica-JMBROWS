using Libreria.LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioServicios : IRepositorio<Servicio>
    {
        IEnumerable<Servicio> BuscarPorNombre(string texto);
        void AsignarSector(int servicioId, int sectorId);
        void QuitarSector(int servicioId, int sectorId);
        void AsignarHabilidad(int servicioId, int habilidadId);
        void QuitarHabilidad(int servicioId, int habilidadId);
        List<Habilidad> ObtenerHabilidadesDeServicio(int servicioId);
        List<Sector> ObtenerSectoresDeServicio(int servicioId);
        List<Servicio> ObtenerPorIds(IEnumerable<int> ids);
        List<Servicio> GetServiciosPorSector(int sectorId);
    }

}
