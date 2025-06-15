using LogicaAplicacion.InterfacesCasosDeUso.ICUDetalleTurno;

namespace LogicaAplicacion.CasosDeUso.CUDetalleTurno
{
    public class CUEliminarDetalleTurno : ICUEliminarDetalleTurno
    {
        private readonly IRepositorioDetalleTurno _repo;

        public CUEliminarDetalleTurno(IRepositorioDetalleTurno repo)
        {
            _repo = repo;
        }

        public void Ejecutar(int id)
        {
            var detalle = _repo.GetById(id);
            if (detalle == null)
                throw new Exception("DetalleTurno no encontrado");

            _repo.Remove(id);
        }
    }
}