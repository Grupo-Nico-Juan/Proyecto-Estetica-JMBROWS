using LogicaAplicacion.InterfacesCasosDeUso.ICUPeriodoLaboral;
using LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesRepositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.CUPeriodoLaboral
{
    public class CUEliminarPeriodoLaboral : ICUEliminarPeriodoLaboral
    {
        private readonly IRepositorioPeriodoLaboral _repo;

        public CUEliminarPeriodoLaboral(IRepositorioPeriodoLaboral repo)
        {
            _repo = repo;
        }

        public void Ejecutar(int id)
        {
            var periodo = _repo.ObtenerPorId(id);
            if (periodo == null)
                throw new PeriodoLaboralException("Periodo laboral no encontrado.");

            _repo.Eliminar(id);
        }
    }
}
