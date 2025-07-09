using LogicaAplicacion.Dtos.TurnoDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUTurno;
using LogicaNegocio.InterfacesRepositorio;
using System.Collections.Generic;
using System.Linq;

namespace LogicaAplicacion.CasosDeUso.CUTurno
{
    public class CUObtenerTurnosFiltrados : ICUObtenerTurnosFiltrados
    {
        private readonly IRepositorioTurnos _repo;

        public CUObtenerTurnosFiltrados(IRepositorioTurnos repo)
        {
            _repo = repo;
        }

        public IEnumerable<TurnoDTO> Ejecutar(TurnoFiltroDTO filtro)
        {
            var turnos = _repo.GetAll();

            if (filtro.EmpleadaId.HasValue)
                turnos = turnos.Where(t => t.EmpleadaId == filtro.EmpleadaId.Value);

            if (filtro.SectorId.HasValue)
                turnos = turnos.Where(t => t.SectorId == filtro.SectorId.Value);

            if (filtro.Estado.HasValue)
                turnos = turnos.Where(t => t.Estado == filtro.Estado.Value);

            return turnos.Select(t => new TurnoDTO
            {
                Id = t.Id,
                FechaHora = t.FechaHora,
                EmpleadaId = t.EmpleadaId,
                ClienteId = t.ClienteId,
                SucursalId = t.SucursalId,
                SectorId = t.SectorId,
                Estado = t.Estado,
                Detalles = t.Detalles.Select(d => new DetalleTurnoDTO { ServicioId = d.ServicioId }).ToList()
            });
        }
    }
}