using LogicaAplicacion.Dtos.TurnoDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUDetalleTurno;
using LogicaNegocio.Entidades;

namespace LogicaAplicacion.CasosDeUso.CUDetalleTurno
{
    public class CUActualizarDetalleTurno : ICUActualizarDetalleTurno
    {
        private readonly IRepositorioDetalleTurno _repo;

        public CUActualizarDetalleTurno(IRepositorioDetalleTurno repo)
        {
            _repo = repo;
        }

        public void Ejecutar(ActualizarDetalleTurnoDTO dto)
        {
            var detalle = _repo.GetById(dto.Id);
            if (detalle == null)
                throw new Exception("DetalleTurno no encontrado");

            detalle.ServicioId = dto.ServicioId;
            detalle.EsValido();

            _repo.Update(dto.Id, detalle);
        }
    }
}