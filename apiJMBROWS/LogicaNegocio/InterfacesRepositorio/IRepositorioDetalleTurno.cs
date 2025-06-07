using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaNegocio.Entidades;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioDetalleTurno
    {
        void Add(DetalleTurno detalle);
        void Update(int id, DetalleTurno detalle);
        void Remove(int id);
        DetalleTurno GetById(int id);
        IEnumerable<DetalleTurno> GetAll();
    }
}
