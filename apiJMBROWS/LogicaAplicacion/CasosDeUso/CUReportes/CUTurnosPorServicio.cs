using LogicaAplicacion.Dtos.ReportesDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUReportes;
using LogicaNegocio.InterfacesRepositorio;
using System.Collections.Generic;
using System.Linq;

namespace LogicaAplicacion.CasosDeUso.CUReportes
{
    public class CUTurnosPorServicio : ICUTurnosPorServicio
    {
        private readonly IRepositorioTurnos _repoTurnos;
        private readonly IRepositorioServicios _repoServicios;

        public CUTurnosPorServicio(IRepositorioTurnos repoTurnos, IRepositorioServicios repoServicios)
        {
            _repoTurnos = repoTurnos;
            _repoServicios = repoServicios;
        }

        public IEnumerable<TurnosPorServicioDTO> Ejecutar(int anio, int mes)
        {
            var servicios = _repoServicios.GetAll().ToDictionary(s => s.Id, s => s.Nombre);
            var conteo = new Dictionary<int, int>();
            var turnos = _repoTurnos.GetAll()
                .Where(t => t.FechaHora.Year == anio && t.FechaHora.Month == mes && !t.Cancelado);

            foreach (var t in turnos)
            {
                foreach (var d in t.Detalles)
                {
                    if (!conteo.ContainsKey(d.ServicioId)) conteo[d.ServicioId] = 0;
                    conteo[d.ServicioId]++;
                }
            }

            return conteo.Select(kvp => new TurnosPorServicioDTO
            {
                Servicio = servicios.TryGetValue(kvp.Key, out var nombre) ? nombre : kvp.Key.ToString(),
                Cantidad = kvp.Value
            });
        }
    }
}
