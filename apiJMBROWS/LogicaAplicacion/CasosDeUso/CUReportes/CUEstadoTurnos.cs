using LogicaAplicacion.Dtos.ReportesDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUReportes;
using LogicaNegocio.InterfacesRepositorio;
using System.Linq;

namespace LogicaAplicacion.CasosDeUso.CUReportes
{
    public class CUEstadoTurnos : ICUEstadoTurnos
    {
        private readonly IRepositorioTurnos _repoTurnos;

        public CUEstadoTurnos(IRepositorioTurnos repoTurnos)
        {
            _repoTurnos = repoTurnos;
        }

        public EstadoTurnosDTO Ejecutar(int anio, int mes)
        {
            var turnos = _repoTurnos.GetAll()
                .Where(t => t.FechaHora.Year == anio && t.FechaHora.Month == mes);
            return new EstadoTurnosDTO
            {
                Realizados = turnos.Count(t => t.Realizado && !t.Cancelado),
                Cancelados = turnos.Count(t => t.Cancelado)
            };
        }
    }
}
