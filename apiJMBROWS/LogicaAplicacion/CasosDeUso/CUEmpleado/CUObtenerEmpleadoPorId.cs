using LogicaAplicacion.Dtos.EmpleadoDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado;
using LogicaNegocio.InterfacesRepositorio;
using System;

namespace LogicaAplicacion.CasosDeUso.CUEmpleado
{
    public class CUObtenerEmpleadoPorId : ICUObtenerEmpleadoPorId
    {
        private readonly IRepositorioUsuarios _repo;

        public CUObtenerEmpleadoPorId(IRepositorioUsuarios repo)
        {
            _repo = repo;
        }

        public EmpleadoDTO Ejecutar(int id)
        {
            var e = _repo.GetEmpleadoById(id);
            if (e == null)
                throw new Exception("Empleado no encontrado");

            return new EmpleadoDTO
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Apellido = e.Apellido,
                Email = e.Email,
                Cargo = e.Cargo,
                SucursalId = e.SucursalId
            };
        }
    }
}