using LogicaAplicacion.Dtos.TurnoDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUTurno;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.Entidades;
using LogicaNegocio.Entidades.Enums;
using System;
using System.Linq;
using Hangfire;

namespace LogicaAplicacion.CasosDeUso.CUTurno
{
    public class CUActualizarTurno : ICUActualizarTurno
    {
        private readonly IRepositorioTurnos _repo;

        public CUActualizarTurno(IRepositorioTurnos repo)
        {
            _repo = repo;
        }

        public void Ejecutar(ActualizarTurnoDTO dto)
        {
            var turno = _repo.GetById(dto.Id)
                       ?? throw new Exception("Turno no encontrado");

            bool fechaCambio = false;

            if (dto.FechaHora.HasValue && dto.FechaHora != turno.FechaHora)
            {
                turno.FechaHora = dto.FechaHora.Value;
                fechaCambio = true;
            }

            if (dto.EmpleadaId.HasValue) turno.EmpleadaId = dto.EmpleadaId.Value;
            if (dto.ClienteId.HasValue) turno.ClienteId = dto.ClienteId.Value;
            if (dto.SucursalId.HasValue) turno.SucursalId = dto.SucursalId.Value;
            if (dto.Estado.HasValue) turno.Estado = dto.Estado.Value;

            if (dto.Detalles != null)
            {
                turno.Detalles.Clear();
                foreach (var det in dto.Detalles)
                    turno.Detalles.Add(new DetalleTurno { ServicioId = det.ServicioId });
            }

            // Validación de solapamiento sólo si cambió fecha/hora o empleada
            if (fechaCambio || dto.EmpleadaId.HasValue)
                turno.EsValido();

            // ─── Hangfire ────────────────────────────────────────────
            if (fechaCambio)
            {
                BackgroundJob.Delete(turno.HangfireId);
                turno.HangfireId = BackgroundJob.Schedule<IWhatsAppService>(
                                       s => s.EnviarRecordatorioAsync(turno.Id),
                                       turno.FechaHora.AddDays(-1));
            }
            else if (dto.Estado.HasValue && turno.Estado != EstadoTurno.Pendiente)
            {
                // Si solo confirmaron/cancelaron: borra recordatorio
                BackgroundJob.Delete(turno.HangfireId);
                turno.HangfireId = null;
            }

            _repo.Update(dto.Id, turno);
        }
    }
}