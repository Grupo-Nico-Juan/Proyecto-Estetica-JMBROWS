using LogicaAplicacion.Dtos.ExtraServicioDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUExtraServicio;
using LogicaNegocio.InterfacesRepositorio;
using System.Linq;

namespace LogicaAplicacion.CasosDeUso.CUExtraServicio
{
    public class CUObtenerExtrasDeServicio : ICUObtenerExtrasDeServicio
    {
        private readonly IRepositorioExtrasServicio _repo;

        public CUObtenerExtrasDeServicio(IRepositorioExtrasServicio repo)
        {
            _repo = repo;
        }

        public IEnumerable<ExtraServicioDTO> Ejecutar(int servicioId)
        {
            return _repo.ObtenerPorServicio(servicioId)
                .Select(e => new ExtraServicioDTO
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    DuracionMinutos = e.DuracionMinutos,
                    Precio = e.Precio,
                    ServicioId = e.ServicioId
                });
        }
    }
}
