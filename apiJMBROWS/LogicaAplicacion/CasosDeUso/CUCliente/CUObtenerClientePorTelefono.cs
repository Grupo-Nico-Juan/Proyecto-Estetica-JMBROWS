using Libreria.LogicaNegocio.Excepciones;
using Libreria.LogicaNegocio.InterfacesRepositorio;
using LogicaAplicacion.Dtos.ClienteDTO;
using LogicaAplicacion.Infraestructura.Helpers;
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
        public CUObtenerClientePorTelefono(IRepositorioClientes repo) => _repo = repo;

        public ClienteDTO? Ejecutar(string telefono)
        {
            // 1) Normalizar / validar el formato
            string telNormalizado;
            try
            {
                telNormalizado = UruguayPhoneHelper.Normalizar(telefono);
            }
            catch (ArgumentException ex)
            {
                // Aquí decidimos propagar con UsuarioException para que
                // el controller responda 400 BadRequest.
                throw new UsuarioException(ex.Message);
            }

            // 2) Consultar repositorio con el formato correcto
            var cliente = _repo.GetByTelefono(telNormalizado);
            if (cliente == null) return null;

            // 3) Mapear a DTO
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
