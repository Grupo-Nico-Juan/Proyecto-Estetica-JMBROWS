using LogicaAplicacion.Dtos.TurnoDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUDetalleTurno;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosDeUso.CUDetalleTurno
{
    public class CUAltaDetalleTurno : ICUAltaDetalleTurno
    {
        private readonly IRepositorioTurnos _repoTurno;
        private readonly IRepositorioServicios _repoServicio;

        public CUAltaDetalleTurno(IRepositorioTurnos repoTurno, IRepositorioServicios repoServicio)
        {
            _repoTurno = repoTurno;
            _repoServicio = repoServicio;
        }

        public void Ejecutar(AltaDetalleTurnoDTO dto)
        {
            var turno = _repoTurno.GetById(dto.TurnoId);
            if (turno == null) throw new Exception("Turno no encontrado.");

            var servicio = _repoServicio.GetById(dto.ServicioId);
            if (servicio == null) throw new Exception("Servicio no válido.");

            var nuevoDetalle = new DetalleTurno
            {
                TurnoId = dto.TurnoId,
                ServicioId = dto.ServicioId,

            };

            turno.Detalles.Add(nuevoDetalle);
            _repoTurno.Update(dto.TurnoId, turno); // o método específico para agregar detalle si tenés
        }
    }

}