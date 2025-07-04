using LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado;
using LogicaNegocio.Entidades.Enums;
using LogicaNegocio.InterfacesRepositorio;
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
        public CUMarcarTurnoComoRealizado(IRepositorioTurnos repo)
        {
            _repo = repo;
        }

        public void Ejecutar(int turnoId)
        {
            var turno = _repo.GetById(turnoId);
            if (turno == null)
                throw new Exception("Turno no encontrado.");

            if (turno.Estado == EstadoTurno.Realizado)
                throw new Exception("El turno ya está marcado como realizado.");

            if (turno.Estado == EstadoTurno.Cancelado)
                throw new Exception("No se puede marcar como realizado un turno cancelado.");

            turno.Estado = EstadoTurno.Realizado;
            _repo.Update(turnoId, turno); // o un método específico si preferís solo cambiar estado
        }
    }
}