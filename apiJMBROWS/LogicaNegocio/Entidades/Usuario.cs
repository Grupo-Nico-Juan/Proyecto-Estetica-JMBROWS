using Libreria.LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesEntidades;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace LogicaNegocio.Entidades
{
    public abstract class Usuario : IValidable, IEquatable<Usuario>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Nombre { get; set; }
        [Required]
        public required string Apellido { get; set; }

        [Required]
        [MinLength(6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[.,;!?])[A-Za-z\d.,;!?]+$", ErrorMessage = "Debe tener mayúsculas, minúsculas, dígitos y puntuación.")]
        public required string Password { get; set; }

        public abstract string Rol { get; }

        public bool Equals(Usuario? other)
        {
            return other != null && Id == other.Id && Email == other.Email;
        }

        public virtual void EsValido()
        {
            if (string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(Apellido))
                throw new UsuarioException("El nombre y el apellido no pueden estar vacíos.");

            if (!Regex.IsMatch(Nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚ\s'-]+$") ||
                !Regex.IsMatch(Apellido, @"^[a-zA-ZáéíóúÁÉÍÓÚ\s'-]+$"))
                throw new UsuarioException("Nombre y Apellido solo deben contener letras, espacios, guiones o apóstrofes.");

            if (!Regex.IsMatch(Email, @"^[^\s@]+@[^\s@]+\.[^\s@]+$"))
                throw new UsuarioException("El email no tiene un formato válido.");
        }

    }
}
