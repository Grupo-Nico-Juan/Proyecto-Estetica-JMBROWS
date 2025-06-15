using LogicaAplicacion.Dtos.TurnoDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUTurno;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;
using System.Collections.Generic;

namespace LogicaAplicacion.CasosDeUso.CUTurno
{
    public class CUAltaTurno : ICUAltaTurno
    {
        private readonly IRepositorioTurnos _repo;

        public CUAltaTurno(IRepositorioTurnos repo)
        {
            _repo = repo;
        }

        public void Ejecutar(AltaTurnoDTO dto)
        {
            var turno = new Turno
            {
                FechaHora = dto.FechaHora,
                EmpleadaId = dto.EmpleadaId,
                ClienteId = dto.ClienteId,
                Realizado = false,
                Detalles = new List<DetalleTurno>()
            };

            foreach (var det in dto.Detalles)
            {
                turno.Detalles.Add(new DetalleTurno
                {
                    ServicioId = det.ServicioId,
                    DuracionMinutos = det.DuracionMinutos,
                    Precio = det.Precio
                });
            }

            turno.EsValido(); // <-- Validación de solapamiento

            _repo.Add(turno);
        }
    }
}