using LogicaAplicacion.Dtos.ReportesDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUReportes;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.Entidades.Enums;
using System.Linq;

namespace LogicaAplicacion.CasosDeUso.CUReportes
{
    public class CUHorarioMayorTurnos : ICUHorarioMayorTurnos
    {
        private readonly IRepositorioTurnos _repoTurnos;

        public CUHorarioMayorTurnos(IRepositorioTurnos repoTurnos)
        {
            _repoTurnos = repoTurnos;
        }

        public HorarioMayorTurnosDTO Ejecutar(int anio, int mes)
        {
            var turnos = _repoTurnos.GetAll()
                .Where(t => t.FechaHora.Year == anio && t.FechaHora.Month == mes && t.Estado != EstadoTurno.Cancelado);
            var grupo = turnos
                .GroupBy(t => t.FechaHora.Hour)
                .Select(g => new { Hora = g.Key, Cantidad = g.Count() })
                .OrderByDescending(g => g.Cantidad)
                .FirstOrDefault();
            if (grupo == null) return new HorarioMayorTurnosDTO { Hora = 0, Cantidad = 0 };
            return new HorarioMayorTurnosDTO { Hora = grupo.Hora, Cantidad = grupo.Cantidad };
        }
    }
}
