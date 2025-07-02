using LogicaAplicacion.Dtos.ExtraServicioDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUExtraServicio;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosDeUso.CUExtraServicio
{
    public class CUObtenerExtraServicioPorId : ICUObtenerExtraServicioPorId
    {
        private readonly IRepositorioExtrasServicio _repo;

        public CUObtenerExtraServicioPorId(IRepositorioExtrasServicio repo)
        {
            _repo = repo;
        }

        public ExtraServicioDTO Ejecutar(int id)
        {
            var extra = _repo.GetById(id);
            if (extra == null) return null;
            return new ExtraServicioDTO
            {
                Id = extra.Id,
                Nombre = extra.Nombre,
                DuracionMinutos = extra.DuracionMinutos,
                Precio = extra.Precio,
                ServicioId = extra.ServicioId
            };
        }
    }
}
