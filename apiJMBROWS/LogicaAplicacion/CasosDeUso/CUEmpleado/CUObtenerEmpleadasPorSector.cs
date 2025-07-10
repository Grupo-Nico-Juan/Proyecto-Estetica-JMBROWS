using LogicaAplicacion.Dtos.EmpleadoDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.CUEmpleado
{
    public class CUObtenerEmpleadasPorSector : ICUObtenerEmpleadasPorSector
    {
        private readonly IRepositorioUsuarios _repo;
        public CUObtenerEmpleadasPorSector(IRepositorioUsuarios repo)
        {
            _repo = repo;
        }


        public IEnumerable<EmpleadoDTO> Ejecutar(int sectorId)
        {
            var empleados = _repo.GetEmpleados()
                .Where(e => e.SectoresAsignados.Any(s => s.Id == sectorId))
                .Select(e => new EmpleadoDTO
                {
                    Id = e.Id,
                    Nombre = e.Nombre,
                    Apellido = e.Apellido,
                    Color = e.Color,
                    Cargo = e.Cargo,
                    SucursalId = e.SucursalId
                });

            return empleados;
        }
    }
}
