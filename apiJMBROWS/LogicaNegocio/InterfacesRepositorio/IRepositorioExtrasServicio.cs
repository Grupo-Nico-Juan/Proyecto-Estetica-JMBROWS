using Libreria.LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.Entidades;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioExtrasServicio : IRepositorio<ExtraServicio>
    {
        IEnumerable<ExtraServicio> ObtenerPorServicio(int servicioId);
    }
}
