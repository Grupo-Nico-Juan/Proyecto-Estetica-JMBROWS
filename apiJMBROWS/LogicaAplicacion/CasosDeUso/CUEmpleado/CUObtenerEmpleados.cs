using LogicaAplicacion.Dtos.EmpleadoDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado;
using LogicaNegocio.InterfacesRepositorio;
using System.Collections.Generic;
using System.Linq;

namespace LogicaAplicacion.CasosDeUso.CUEmpleado
{
    public class CUObtenerEmpleados : ICUObtenerEmpleados
    {
        private readonly IRepositorioUsuarios _repo;

        public CUObtenerEmpleados(IRepositorioUsuarios repo)
        {
            _repo = repo;
        }

        public IEnumerable<EmpleadoDTO> Ejecutar()
        {
            return _repo.GetEmpleados().Select(e => new EmpleadoDTO
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Apellido = e.Apellido,
                Email = e.Email,
                Cargo = e.Cargo,
                SucursalId = e.SucursalId
            });
        }
    }
}