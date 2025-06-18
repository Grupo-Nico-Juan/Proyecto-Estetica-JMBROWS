using Libreria.LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioSectores : IRepositorio<Sector>
    {
        IEnumerable<Sector> ObtenerPorSucursal(int idSucursal);
        void Add(Sector sector);
        void Update(int id, Sector sector);
        void Remove(int id);
        Sector GetById(int id);
        IEnumerable<Sector> GetAll();

        IEnumerable<Sector> ObtenerSectoresConServiciosPorSucursal(int sucursalId);
    }
}
