using LogicaAplicacion.Dtos.HabilidadDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado;
using LogicaNegocio.InterfacesRepositorio;
using System.Collections.Generic;
using System.Linq;

namespace LogicaAplicacion.CasosDeUso.CUEmpleado
{
    public class CUObtenerHabilidadesDeEmpleado : ICUObtenerHabilidadesDeEmpleado
    {
        private readonly IRepositorioUsuarios _repo;

        public CUObtenerHabilidadesDeEmpleado(IRepositorioUsuarios repo)
        {
            _repo = repo;
        }

        public IEnumerable<HabilidadDTO> Ejecutar(int empleadoId)
        {
            var empleado = _repo.GetEmpleadoById(empleadoId);
            if (empleado == null)
                throw new System.Exception("Empleado no encontrado");

            return empleado.Habilidades.Select(h => new HabilidadDTO
            {
                Id = h.Id,
                Nombre = h.Nombre,
                Descripcion = h.Descripcion
            });
        }
    }
}