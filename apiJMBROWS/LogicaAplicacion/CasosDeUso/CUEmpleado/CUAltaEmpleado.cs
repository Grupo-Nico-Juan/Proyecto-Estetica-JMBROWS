using LogicaAplicacion.Dtos.EmpleadoDTO;
using LogicaAplicacion.InterfacesCasosDeUso.ICUEmpleado;
using LogicaNegocio.Entidades;
using LogicaNegocio.InterfacesRepositorio;

namespace LogicaAplicacion.CasosDeUso.CUEmpleado
{
    public class CUAltaEmpleado : ICUAltaEmpleado
    {
        private readonly IRepositorioUsuarios _repo;

        public CUAltaEmpleado(IRepositorioUsuarios repo)
        {
            _repo = repo;
        }

        public void Ejecutar(AltaEmpleadoDTO dto)
        {
            var nuevo = new Empleado
            {
                Nombre = dto.Nombre,
                Apellido = dto.Apellido,
                Email = dto.Email,
                PasswordPlano = dto.PasswordPlano,
                Password = HashPassword(dto.PasswordPlano), // Fix: Set the required 'Password' property
                Cargo = dto.Cargo
            };
            nuevo.EsValidoEmpleado();
            _repo.AddEmpleado(nuevo);
        }

        private string HashPassword(string plainPassword)
        {
            // Implement password hashing logic here
            // For example, use a hashing library or custom logic
            return plainPassword; // Replace with actual hashed password
        }
    }
}