using Libreria.LogicaNegocio.InterfacesRepositorio;
using LogicaAplicacion.Dtos.ClienteDTO;
using LogicaAplicacion.Dtos.EmpleadoDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUCliente;
using LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.CUCliente
{
    public class CUObtenerClientePorId : ICUObtenerClientePorId
    {

        private readonly IRepositorioClientes _repo;
        public CUObtenerClientePorId(IRepositorioClientes repo) { _repo = repo; }

        public ClienteDTO? Ejecutar(int clienteId)
        {
            var cliente = _repo.GetById(clienteId);
            if (cliente == null) return null;
            return new ClienteDTO
            {
                Id = cliente.Id,
                Telefono = cliente.Telefono,
                Email = cliente.Email,
                Nombre = cliente.Nombre,
                Apellido = cliente.Apellido
            };
        }
    }
}
