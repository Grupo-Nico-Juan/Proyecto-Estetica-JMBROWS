using LogicaAplicacion.Dtos.EmpleadoDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosDeUso.CUEmpleado
{
    public class CUQuitarSectorEmpleado : ICUQuitarSectorEmpleado
    {
        private readonly IRepositorioUsuarios _repo;

        public CUQuitarSectorEmpleado(IRepositorioUsuarios repo)
        {
            _repo = repo;
        }

        public void Ejecutar(EmpleadoSectorDTO dto)
        {
            _repo.QuitarSector(dto.EmpleadoId, dto.SectorId);
        }
    }
}