using LogicaAplicacion.Dtos.TurnoDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUTurno;
using LogicaNegocio.InterfacesRepositorio;
using System.Linq;

namespace LogicaAplicacion.CasosDeUso.CUTurno
{
    public class CUObtenerTurnoPorId : ICUObtenerTurnoPorId
    {
        private readonly IRepositorioTurnos _repo;

        public CUObtenerTurnoPorId(IRepositorioTurnos repo)
        {
            _repo = repo;
        }

        public TurnoDTO Ejecutar(int id)
        {
            var t = _repo.GetById(id);
            if (t == null) throw new System.Exception("Turno no encontrado");

            return new TurnoDTO
            {
                Id = t.Id,
                FechaHora = t.FechaHora,
                EmpleadaId = t.EmpleadaId,
                ClienteId = t.ClienteId,
                Realizado = t.Realizado,
                Detalles = t.Detalles.Select(d => new DetalleTurnoDTO
                {
                    ServicioId = d.ServicioId,
                    DuracionMinutos = d.DuracionMinutos,
                    Precio = d.Precio
                }).ToList()
            };
        }
    }
}