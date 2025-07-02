using LogicaAplicacion.Dtos.ExtraServicioDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUExtraServicio;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosDeUso.CUExtraServicio
{
    public class CUActualizarExtraServicio : ICUActualizarExtraServicio
    {
        private readonly IRepositorioExtrasServicio _repo;

        public CUActualizarExtraServicio(IRepositorioExtrasServicio repo)
        {
            _repo = repo;
        }

        public void Ejecutar(ExtraServicioDTO dto)
        {
            var extra = _repo.GetById(dto.Id);
            if (extra == null) return;
            extra.Nombre = dto.Nombre;
            extra.DuracionMinutos = dto.DuracionMinutos;
            extra.Precio = dto.Precio;
            _repo.Update(dto.Id, extra);
        }
    }
}
