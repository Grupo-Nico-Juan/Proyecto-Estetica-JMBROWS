using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;
using Libreria.LogicaNegocio.Excepciones;

namespace LogicaNegocio.Entidades
{
    public class Cliente
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

        [JsonIgnore]
        [NotMapped]
        public string PasswordPlano { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[.,;!?])[A-Za-z\d.,;!?]+$", ErrorMessage = "Debe tener mayúsculas, minúsculas, dígitos y puntuación.")]
        public required string Password { get; set; }


        public List<Turno> Turnos { get; set; } = new();

        public bool Equals(Cliente? other)
        {
            return other != null && Id == other.Id && Email == other.Email;
        }

        public virtual void EsValido()
        {
            if (string.IsNullOrWhiteSpace(Email) ||
                !Regex.IsMatch(Email, @"^[^\s@]+@[^\s@]+\.[^\s@]+$"))
                throw new UsuarioException("El email no tiene un formato válido.");

            if (string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(Apellido))
                throw new UsuarioException("El nombre y el apellido no pueden estar vacíos.");

            if (!Regex.IsMatch(Nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚ\s'-]+$") ||
                !Regex.IsMatch(Apellido, @"^[a-zA-ZáéíóúÁÉÍÓÚ\s'-]+$"))
                throw new UsuarioException("Nombre y apellido solo pueden contener letras, espacios, guiones o apóstrofes.");

            if (string.IsNullOrWhiteSpace(PasswordPlano) || PasswordPlano.Length < 6 ||
                !Regex.IsMatch(PasswordPlano, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[.,;!?])[A-Za-z\d.,;!?]+$"))
            {
                throw new UsuarioException("La contraseña debe tener al menos una letra mayúscula, una minúscula, un número y un signo de puntuación (.,;!?).");
            }
        }
    }
}


