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

        public IEnumerable<DetalleTurnoDTO> Ejecutar()
        {
            return _repo.GetAll().Select(detalle => new DetalleTurnoDTO
            {
                Id = detalle.Id,
                TurnoId = detalle.TurnoId,
                ServicioId = detalle.ServicioId,
                DuracionMinutos = detalle.DuracionMinutos,
                Precio = detalle.Precio
            });
        }
    }
}