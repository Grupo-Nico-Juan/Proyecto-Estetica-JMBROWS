using LogicaAplicacion.Dtos.EmpleadoDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado;
using LogicaNegocio.InterfacesRepositorio;
using System;

namespace LogicaAplicacion.CasosDeUso.CUEmpleado
{
    public class CUActualizarEmpleado : ICUActualizarEmpleado
    {
        private readonly IRepositorioUsuarios _repo;

        public CUActualizarEmpleado(IRepositorioUsuarios repo)
        {
            _repo = repo;
        }

        public void Ejecutar(ActualizarEmpleadoDTO dto)
        {
            var empleado = _repo.GetEmpleadoById(dto.Id);
            if (empleado == null)
                throw new Exception("Empleado no encontrado");

            empleado.Nombre = dto.Nombre;
            empleado.Apellido = dto.Apellido;
            empleado.Color = dto.Color;
            empleado.Cargo = dto.Cargo;
            empleado.SucursalId = dto.SucursalId;
            empleado.EsValidoEmpleado();

            _repo.UpdateEmpleado(empleado.Id, empleado);
        }
    }
}