using LogicaAplicacion.Dtos.ExtraServicioDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUExtraServicio;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosDeUso.CUExtraServicio
{
    public class CUAltaExtraServicio : ICUAltaExtraServicio
    {
        private readonly IRepositorioExtrasServicio _repo;

        public CUAltaExtraServicio(IRepositorioExtrasServicio repo)
        {
            _repo = repo;
        }

        public void Ejecutar(AltaExtraServicioDTO dto)
        {
            var extra = new ExtraServicio
            {
                Nombre = dto.Nombre,
                DuracionMinutos = dto.DuracionMinutos,
                Precio = dto.Precio,
                ServicioId = dto.ServicioId
            };
            _repo.Add(extra);
        }
    }
}
