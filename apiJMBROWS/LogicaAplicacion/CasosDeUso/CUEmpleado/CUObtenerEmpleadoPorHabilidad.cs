using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using LogicaAplicacion.Dtos.EmpleadoDTO;
using System.Collections.Generic;
using System.Linq;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosDeUso.CUEmpleado
{
    public class CUObtenerEmpleadoPorHabilidad : ICUObtenerEmpleadoPorHabilidad
    {
        private readonly IRepositorioUsuarios _repo;
        public CUObtenerEmpleadoPorHabilidad(IRepositorioUsuarios repo)
        {
            _repo = repo;
        }

        public IEnumerable<EmpleadoDTO> Ejecutar(int habilidadId)
        {
            var empleados = _repo.GetEmpleados()
                .Where(e => e.Habilidades.Any(h => h.Id == habilidadId))
                .Select(e => new EmpleadoDTO
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Apellido = e.Apellido,
                    Email = e.Email,
                    Cargo = e.Cargo
                });

            return empleados;
        }
    }
}