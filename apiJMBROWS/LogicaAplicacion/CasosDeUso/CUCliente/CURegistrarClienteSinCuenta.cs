using Libreria.LogicaNegocio.InterfacesRepositorio;
using LogicaAccesoDatos.Migrations;
using LogicaAplicacion.Dtos.ClienteDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUCliente;
using LogicaNegocio.Entidades;


namespace LogicaAplicacion.CasosDeUso.CUCliente
{
    public class CURegistrarClienteSinCuenta : ICURegistrarClienteSinCuenta
    {
        private readonly IRepositorioClientes _repo;

        public CURegistrarClienteSinCuenta(IRepositorioClientes repo)
        {
            _repo = repo;
        }

        public ReservaClienteDTO Ejecutar(ReservaClienteDTO dto)
        {
            // Validación mínima
            if (string.IsNullOrWhiteSpace(dto.Nombre) ||
                string.IsNullOrWhiteSpace(dto.Apellido) ||
                string.IsNullOrWhiteSpace(dto.Telefono))
            {
                throw new ArgumentException("Nombre, apellido y teléfono son obligatorios.");
            }

            // Verificar si ya existe cliente con ese teléfono
            var existente = _repo.GetByTelefono(dto.Telefono);
            if (existente != null)
            {
                // Solo devolver datos básicos en un nuevo objeto Cliente
                return new ReservaClienteDTO
                {
                    Id = existente.Id,
                    Telefono = existente.Telefono,
                    Email = existente.Email,
                    Nombre = existente.Nombre,
                    Apellido = existente.Apellido
                };
            }

            // Crear nuevo cliente como ocasional
            var nuevo = new Cliente
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Telefono = dto.Telefono,
                Email = dto.Email,
                EsRegistrado = false // Campo booleano para distinguir cliente con cuenta
            };

            _repo.Add(nuevo);
            return new ReservaClienteDTO
            {
                Id = nuevo.Id,
                Telefono = nuevo.Telefono,
                Email = nuevo.Email,
                Nombre = nuevo.Nombre,
                Apellido = nuevo.Apellido
            };
        }
    }
}
