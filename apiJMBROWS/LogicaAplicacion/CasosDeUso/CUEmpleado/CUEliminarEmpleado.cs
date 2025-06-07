using LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado;
using LogicaNegocio.InterfacesRepositorio;
using System;

namespace LogicaAplicacion.CasosDeUso.CUEmpleado
{
    public class CUEliminarEmpleado : ICUEliminarEmpleado
    {
        private readonly IRepositorioUsuarios _repo;

        public CUEliminarEmpleado(IRepositorioUsuarios repo)
        {
            _repo = repo;
        }

        public void Ejecutar(int id)
        {
            var empleado = _repo.GetEmpleadoById(id);
            if (empleado == null)
                throw new Exception("Empleado no encontrado");

            _repo.DeleteEmpleado(id);
        }
    }
}