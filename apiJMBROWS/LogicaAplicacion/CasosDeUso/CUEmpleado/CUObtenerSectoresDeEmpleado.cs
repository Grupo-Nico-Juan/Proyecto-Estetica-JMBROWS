using LogicaAplicacion.Dtos.SectorDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado;
using LogicaNegocio.InterfacesRepositorio;
using System.Collections.Generic;
using System.Linq;

namespace LogicaAplicacion.CasosDeUso.CUEmpleado
{
    public class CUObtenerSectoresDeEmpleado : ICUObtenerSectoresDeEmpleado
    {
        private readonly IRepositorioUsuarios _repo;

        public CUObtenerSectoresDeEmpleado(IRepositorioUsuarios repo)
        {
            _repo = repo;
        }

        public IEnumerable<SectorDTO> Ejecutar(int empleadoId)
        {
            var empleado = _repo.GetEmpleadoById(empleadoId);
            if (empleado == null)
                throw new System.Exception("Empleado no encontrado");

            return empleado.SectoresAsignados.Select(s => new SectorDTO
            {
                Id = s.Id,
                Nombre = s.Nombre,
                SucursalId = s.SucursalId 
            });
        }
    }
}