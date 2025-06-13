using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioPeriodoLaboral
    {
        void Agregar(PeriodoLaboral periodo);
        IEnumerable<PeriodoLaboral> ObtenerPorEmpleada(int empleadaId);
        PeriodoLaboral? ObtenerPorId(int id);
        void Eliminar(int id);
        void Modificar(PeriodoLaboral periodo);
        IEnumerable<PeriodoLaboral> ObtenerTodos();
    }
}

