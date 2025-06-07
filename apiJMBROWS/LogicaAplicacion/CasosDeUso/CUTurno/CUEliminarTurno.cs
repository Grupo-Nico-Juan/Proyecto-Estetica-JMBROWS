using LogicaAplicacion.InterfacesCasosDeUso.ICUTurno;
using LogicaNegocio.InterfacesRepositorio;
using System;

namespace LogicaAplicacion.CasosDeUso.CUTurno
{
    public class CUEliminarTurno : ICUEliminarTurno
    {
        private readonly IRepositorioTurnos _repo;

        public CUEliminarTurno(IRepositorioTurnos repo)
        {
            _repo = repo;
        }

        public void Ejecutar(int id)
        {
            var turno = _repo.GetById(id);
            if (turno == null)
                throw new Exception("Turno no encontrado");

            _repo.Remove(id);
        }
    }
}