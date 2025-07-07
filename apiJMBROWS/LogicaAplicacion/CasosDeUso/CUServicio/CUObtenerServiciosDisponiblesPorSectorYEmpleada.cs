using LogicaAplicacion.Dtos.ExtraServicioDTO;
using LogicaAplicacion.Dtos.ServicioDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUServicio;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.CUServicio
{
    public class CUObtenerServiciosDisponiblesPorSectorYEmpleada : ICUObtenerServiciosDisponiblesPorSectorYEmpleada
    {
        private readonly IRepositorioServicios _repoServicios;
        private readonly IRepositorioUsuarios _repoUsuarios;

        public CUObtenerServiciosDisponiblesPorSectorYEmpleada(IRepositorioServicios repoServicios, IRepositorioUsuarios repoUsuarios)
        {
            _repoServicios = repoServicios;
            _repoUsuarios = repoUsuarios;
        }

        public IEnumerable<ServicioDTO> Ejecutar(FiltroServiciosDTO filtro)
        {
            var empleado = _repoUsuarios.GetById(filtro.EmpleadaId) as Empleado;

            if (empleado == null)
                throw new Exception("Empleado no encontrado o no es un empleado válido.");

            var habilidadesEmpleado = empleado.Habilidades.Select(h => h.Id).ToList();

            var servicios = _repoServicios.GetAll()
                .Where(s => s.Sectores.Any(sector => sector.Id == filtro.SectorId)
                         && s.Habilidades.All(h => habilidadesEmpleado.Contains(h.Id)))
                .Select(s => new ServicioDTO
                {
                    Id = s.Id,
                    Nombre = s.Nombre,
                    Descripcion = s.Descripcion,
                    DuracionMinutos = s.DuracionMinutos,
                    Precio = s.Precio,
                    Extras = s.Extras.Select(e => new ServiciosExtrasDTO
                    {
                        Id = e.Id,
                        Nombre = e.Nombre,
                        DuracionMinutos = e.DuracionMinutos,
                        Precio = e.Precio,
                        ServicioId = s.Id
                    }).ToList()
                });

            return servicios;
        }

    }
}
