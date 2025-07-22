using Libreria.LogicaNegocio.InterfacesRepositorio;
using LogicaAplicacion.Dtos;
using LogicaAplicacion.Dtos.ClienteDTO;
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
            return repoClientes.GetAll().Select(c => new ClienteDTO
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Apellido = c.Apellido,
                Telefono = c.Telefono,
                Email = c.Email ?? string.Empty,
                EsRegistrado = c.EsRegistrado
            });
        }
    }
}
