using LogicaAplicacion.Dtos.TurnoDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUDetalleTurno;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System.Linq;

namespace LogicaAplicacion.CasosDeUso.CUDetalleTurno
{
    public class CUAltaDetalleTurno : ICUAltaDetalleTurno
    {
        private readonly IRepositorioTurnos _repoTurno;
        private readonly IRepositorioServicios _repoServicio;
        private readonly IRepositorioExtrasServicio _repoExtras;

        public CUAltaDetalleTurno(IRepositorioTurnos repoTurno, IRepositorioServicios repoServicio, IRepositorioExtrasServicio repoExtras)
        {
            _repoTurno = repoTurno;
            _repoServicio = repoServicio;
            _repoExtras = repoExtras;
        }

        public void Ejecutar(AltaDetalleTurnoDTO dto)
        {
            var turno = _repoTurno.GetById(dto.TurnoId) ?? throw new Exception("Turno no encontrado.");
            var servicio = _repoServicio.GetById(dto.ServicioId) ?? throw new Exception("Servicio no valido.");

            var nuevoDetalle = new DetalleTurno
            {
                TurnoId = dto.TurnoId,
                ServicioId = dto.ServicioId,
            };

            if (dto.ExtrasIds != null && dto.ExtrasIds.Any())
            {
                var extras = _repoExtras.GetAll().Where(e => dto.ExtrasIds.Contains(e.Id)).ToList();
                nuevoDetalle.Extras = extras;
            }

            turno.Detalles.Add(nuevoDetalle);
            _repoTurno.Update(dto.TurnoId, turno);
        }
    }
}
