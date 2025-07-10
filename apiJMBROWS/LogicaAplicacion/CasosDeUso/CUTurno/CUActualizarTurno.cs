using LogicaAplicacion.Dtos.TurnoDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUTurno;
using LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.Entidades;
using LogicaNegocio.Entidades.Enums;
using System;
using System.Linq;

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
            var turno = _repo.GetById(dto.Id);
            if (turno == null)
                throw new Exception("Turno no encontrado");

            turno.FechaHora = dto.FechaHora;
            turno.EmpleadaId = dto.EmpleadaId;
            turno.ClienteId = dto.ClienteId;
            turno.Estado = dto.Estado;
            turno.SucursalId = dto.SucursalId;


            turno.Detalles.Clear();
            foreach (var det in dto.Detalles)
            {
                turno.Detalles.Add(new DetalleTurno
                {
                    ServicioId = det.ServicioId,

                });
            }

            turno.EsValido(); // <-- Validación de solapamiento

            _repo.Update(dto.Id, turno);
        }
    }
}