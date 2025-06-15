using Libreria.LogicaNegocio.InterfacesRepositorio;
using LogicaAplicacion.Dtos;
using LogicaAplicacion.Dtos.EmpleadoDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUCliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.CUCliente
{
    public class CUGetClientes : ICUGetClientes
    {
        private readonly IRepositorioClientes repoClientes;

        public CUGetClientes(IRepositorioClientes repo)
        {
            repoClientes = repo;
        }

        public IEnumerable<ClienteDTO> Ejecutar()
        {
            return repoClientes.GetAll().Select(e => new ClienteDTO
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Apellido = e.Apellido,
                Email = e.Email,
            });
        }
    }
}
