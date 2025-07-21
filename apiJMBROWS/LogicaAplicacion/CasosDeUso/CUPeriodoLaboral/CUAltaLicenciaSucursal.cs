using LogicaAplicacion.Dtos.PeriodoLaboralDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUPeriodoLaboral;
using LogicaNegocio.Entidades;
using LogicaNegocio.Entidades.Enums;
using LogicaNegocio.InterfacesRepositorio;
using System.Linq;

namespace LogicaAplicacion.CasosDeUso.CUPeriodoLaboral
{
    public class CUAltaLicenciaSucursal : ICUAltaLicenciaSucursal
    {
        private readonly IRepositorioUsuarios _repoUsuarios;
        private readonly IRepositorioPeriodoLaboral _repoPeriodos;

        public CUAltaLicenciaSucursal(IRepositorioUsuarios repoUsuarios, IRepositorioPeriodoLaboral repoPeriodos)
        {
            _repoUsuarios = repoUsuarios;
            _repoPeriodos = repoPeriodos;
        }

        public void Ejecutar(AltaLicenciaSucursalDTO dto)
        {
            var empleadas = _repoUsuarios.GetEmpleados()
                .Where(e =>
                    (e.SucursalId.HasValue && dto.SucursalIds.Contains(e.SucursalId.Value)) ||
                    e.SectoresAsignados.Any(s => dto.SucursalIds.Contains(s.SucursalId)))
                .ToList();

            foreach (var emp in empleadas)
            {

                var licenciasActuales = _repoPeriodos
                    .ObtenerPorEmpleada(emp.Id)
                    .Where(p => p.Tipo == TipoPeriodoLaboral.Licencia)
                    .ToList();

                bool yaTieneLicenciaEnEsePeriodo = licenciasActuales.Any(p =>
                    (dto.Desde >= p.Desde && dto.Desde < p.Hasta) || // inicia dentro de otra
                    (dto.Hasta > p.Desde && dto.Hasta <= p.Hasta) || // termina dentro de otra
                    (dto.Desde <= p.Desde && dto.Hasta >= p.Hasta)   // la nueva cubre a la anterior
                );

                if (yaTieneLicenciaEnEsePeriodo)
                    continue;

                var licencia = new PeriodoLaboral
                {
                    EmpleadaId = emp.Id,
                    Tipo = TipoPeriodoLaboral.Licencia,
                    Desde = dto.Desde,
                    Hasta = dto.Hasta,
                    Motivo = dto.Motivo
                };

                licencia.EsValido();
                _repoPeriodos.Agregar(licencia);
            }
        }

    }
}
