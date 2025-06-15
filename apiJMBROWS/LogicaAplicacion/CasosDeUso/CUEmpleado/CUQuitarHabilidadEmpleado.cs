using LogicaAplicacion.Dtos.EmpleadoDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosDeUso.CUEmpleado
{
    public class CUQuitarHabilidadEmpleado : ICUQuitarHabilidadEmpleado
    {
        private readonly IRepositorioUsuarios _repo;

        public CUQuitarHabilidadEmpleado(IRepositorioUsuarios repo)
        {
            _repo = repo;
        }

        public void Ejecutar(EmpleadoHabilidadDTO dto)
        {
            _repo.QuitarHabilidad(dto.EmpleadoId, dto.HabilidadId);
        }
    }
}