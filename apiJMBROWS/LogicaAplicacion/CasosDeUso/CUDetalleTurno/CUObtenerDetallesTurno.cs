using LogicaAplicacion.Dtos.TurnoDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUDetalleTurno;
using System.Collections.Generic;
using System.Linq;

namespace LogicaAplicacion.CasosDeUso.CUDetalleTurno
{
    public class CUObtenerDetallesTurno : ICUObtenerDetallesTurno
    {
        private readonly IRepositorioDetalleTurno _repo;

        public CUObtenerDetallesTurno(IRepositorioDetalleTurno repo)
        {
            _repo = repo;
        }

        public IEnumerable<DetalleTurnoDTO> Ejecutar(int turnoId)
        {
            return _repo.GetByTurnoId(turnoId).Select(detalle => new DetalleTurnoDTO
            {
                Id = detalle.Id,
                TurnoId = detalle.TurnoId,
                ServicioId = detalle.ServicioId,

            });
        }
    }
}