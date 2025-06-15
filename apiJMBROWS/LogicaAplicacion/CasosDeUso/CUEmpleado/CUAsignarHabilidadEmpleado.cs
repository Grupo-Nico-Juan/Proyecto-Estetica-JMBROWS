using LogicaAplicacion.Dtos.EmpleadoDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosDeUso.CUEmpleado
{
    public class CUAsignarHabilidadEmpleado : ICUAsignarHabilidadEmpleado
    {
        private readonly IRepositorioUsuarios _repo;

        public CUAsignarHabilidadEmpleado(IRepositorioUsuarios repo)
        {
            _repo = repo;
        }

        public void Ejecutar(EmpleadoHabilidadDTO dto)
        {
            _repo.AsignarHabilidad(dto.EmpleadoId, dto.HabilidadId);
        }
    }
}