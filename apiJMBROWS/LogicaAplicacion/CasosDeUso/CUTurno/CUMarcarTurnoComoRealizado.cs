using LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado;
using LogicaNegocio.Entidades.Enums;
using LogicaNegocio.InterfacesRepositorio;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.CUTurno
{
    public class CUMarcarTurnoComoRealizado : ICUMarcarTurnoComoRealizado
    {
        private readonly IRepositorioTurnos _repo;
        private readonly IConfiguration _config;
        private readonly ILogger<CUMarcarTurnoComoRealizado> _logger;

        public CUMarcarTurnoComoRealizado(IRepositorioTurnos repo, IConfiguration config, ILogger<CUMarcarTurnoComoRealizado> logger)
        {
            _repo = repo;
            _config = config;
            _logger = logger;
        }

        public void Ejecutar(int turnoId, string pin)
        {
            var pinConfig = _config["ConfirmacionTurno:Pin"];
            if (pinConfig == null || pin != pinConfig)
                throw new Exception("PIN inválido");
            var turno = _repo.GetById(turnoId) ?? throw new Exception("Turno no encontrado");

            if (turno.Estado == EstadoTurno.Realizado)
                throw new Exception("El turno ya está marcado como realizado.");

            if (turno.Estado == EstadoTurno.Cancelado)
                throw new Exception("No se puede marcar como realizado un turno cancelado.");

            turno.Estado = EstadoTurno.Realizado;
            _repo.Update(turnoId, turno); _logger.LogInformation("Turno {Id} confirmado a las {Fecha}", turnoId, DateTime.UtcNow);
        }
    }
}