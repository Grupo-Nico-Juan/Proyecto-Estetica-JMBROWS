using Libreria.LogicaNegocio.InterfacesRepositorio;
using LogicaNegocio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorioSucursales : IRepositorio<Sucursal>
    {
        IEnumerable<Sucursal> BuscarPorNombre(string nombre);
    }
}
