using LogicaAplicacion.Dtos.EmpleadoDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosDeUso.CUEmpleado
{
    public class CUAsignarSectorEmpleado : ICUAsignarSectorEmpleado
    {
        private readonly IRepositorioUsuarios _repo;

        public CUAsignarSectorEmpleado(IRepositorioUsuarios repo)
        {
            _repo = repo;
        }

        public void Ejecutar(EmpleadoSectorDTO dto)
        {
            _repo.AsignarSector(dto.EmpleadoId, dto.SectorId);
        }
    }
}