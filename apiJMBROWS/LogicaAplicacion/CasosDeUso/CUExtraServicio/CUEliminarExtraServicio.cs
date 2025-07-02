using LogicaAplicacion.InterfacesCasosDeUso.ICUExtraServicio;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosDeUso.CUExtraServicio
{
    public class CUEliminarExtraServicio : ICUEliminarExtraServicio
    {
        private readonly IRepositorioExtrasServicio _repo;

        public CUEliminarExtraServicio(IRepositorioExtrasServicio repo)
        {
            _repo = repo;
        }

        public void Ejecutar(int id)
        {
            _repo.Remove(id);
        }
    }
}
