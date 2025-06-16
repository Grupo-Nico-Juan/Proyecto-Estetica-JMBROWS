using LogicaAplicacion.Dtos.TurnoDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUDetalleTurno;

namespace LogicaAplicacion.CasosDeUso.CUDetalleTurno
{
    public class CUObtenerDetalleTurnoPorId : ICUObtenerDetalleTurnoPorId
    {
        private readonly IRepositorioDetalleTurno _repo;

        public CUObtenerDetalleTurnoPorId(IRepositorioDetalleTurno repo)
        {
            _repo = repo;
        }

        public DetalleTurnoDTO Ejecutar(int id)
        {
            var detalle = _repo.GetById(id);
            if (detalle == null)
                throw new Exception("DetalleTurno no encontrado");

            return new DetalleTurnoDTO
            {
                Id = detalle.Id,
                TurnoId = detalle.TurnoId,
                ServicioId = detalle.ServicioId,

            };
        }
    }
}