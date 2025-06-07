using LogicaAplicacion.Dtos.ServicioDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUServicio;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosDeUso.CUServicio
{
    public class CUAltaServicio : ICUAltaServicio
    {
        private readonly IRepositorioServicios _repo;

        public CUAltaServicio(IRepositorioServicios repo)
        {
            _repo = repo;
        }

        public void Ejecutar(AltaServicioDTO dto)
        {
            var nuevo = new Servicio
            {
                Nombre = dto.Nombre,
                Descripcion = dto.Descripcion,
                DuracionMinutos = dto.DuracionMinutos,
                Precio = dto.Precio
            };
            nuevo.EsValido();
            _repo.Add(nuevo);
        }
    }
}