using Libreria.LogicaNegocio.InterfacesRepositorio;
using LogicaAplicacion.InterfacesCasosDeUso.ICUCliente;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogicaAplicacion.CasosDeUso.CUCliente
{
    public class CUObtenerClientePorTelefono : ICUObtenerClientePorTelefono
    {
        private readonly IRepositorioClientes _repo;
        public CUObtenerClientePorTelefono(IRepositorioClientes repo) { _repo = repo; }

        public ClienteDTO? Ejecutar(string telefono)
        {
            var cliente = _repo.GetByTelefono(telefono);
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
