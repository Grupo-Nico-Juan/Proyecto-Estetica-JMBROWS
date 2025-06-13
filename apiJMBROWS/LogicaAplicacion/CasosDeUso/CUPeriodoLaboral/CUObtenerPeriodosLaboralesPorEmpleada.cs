using LogicaAplicacion.Dtos.PeriodoLaboralDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUPeriodoLaboral;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosDeUso.CUPeriodoLaboral
{
    public class CUObtenerPeriodosLaboralesPorEmpleada : ICUObtenerPeriodosLaboralesPorEmpleada
    {
        private readonly IRepositorioPeriodoLaboral _repo;

        public CUObtenerPeriodosLaboralesPorEmpleada(IRepositorioPeriodoLaboral repo)
        {
            _repo = repo;
        }

        public IEnumerable<PeriodoLaboralDTO> Ejecutar(int empleadaId)
        {
            var periodos = _repo.ObtenerPorEmpleada(empleadaId);

            return periodos.Select(p => new PeriodoLaboralDTO
            {
                Id = p.Id,
                EmpleadaId = p.EmpleadaId,
                Desde = p.Desde,
                Hasta = p.Hasta,
                Motivo = p.Motivo,
                EsLicencia = p.EsLicencia
            });
        }
    }
}
