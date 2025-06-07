using LogicaAplicacion.Dtos.TurnoDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUDetalleTurno;
using LogicaNegocio.Entidades;

namespace LogicaAplicacion.CasosDeUso.CUDetalleTurno
{
    public class CUAltaDetalleTurno : ICUAltaDetalleTurno
    {
        private readonly IRepositorioDetalleTurno _repo;

        public CUAltaDetalleTurno(IRepositorioDetalleTurno repo)
        {
            _repo = repo;
        }

        public void Ejecutar(AltaDetalleTurnoDTO dto)
        {
            var detalle = new DetalleTurno
            {
                TurnoId = dto.TurnoId,
                ServicioId = dto.ServicioId,
                DuracionMinutos = dto.DuracionMinutos,
                Precio = dto.Precio
            };
            detalle.EsValido();
            _repo.Add(detalle);
        }
    }
}