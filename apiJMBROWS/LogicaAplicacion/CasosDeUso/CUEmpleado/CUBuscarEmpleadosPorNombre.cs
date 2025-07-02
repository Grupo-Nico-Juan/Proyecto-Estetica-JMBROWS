using LogicaAplicacion.Dtos.EmpleadoDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System.Collections.Generic;
using System.Linq;

namespace LogicaAplicacion.CasosDeUso.CUEmpleado
{
    public class CUBuscarEmpleadosPorNombre : ICUBuscarEmpleadosPorNombre
    {
        private readonly IRepositorioUsuarios _repo;

        public CUBuscarEmpleadosPorNombre(IRepositorioUsuarios repo)
        {
            _repo = repo;
        }

        public IEnumerable<EmpleadoDTO> Ejecutar(string texto)
        {
            // Se asume que GetEmpleados() devuelve todos los empleados
            return _repo.GetEmpleados()
                .Where(e => e.Nombre.Contains(texto) || e.Apellido.Contains(texto))
                .Select(e => new EmpleadoDTO
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