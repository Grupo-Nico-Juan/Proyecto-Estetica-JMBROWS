using Libreria.LogicaNegocio.Excepciones;
using LogicaNegocio.Entidades;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Libreria.LogicaNegocio.Entidades
{
    public class Administrador : Usuario
    {
        public override string Rol => "Administrador";
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        [MinLength(6)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[.,;!?])[A-Za-z\d.,;!?]+$", ErrorMessage = "Debe tener mayúsculas, minúsculas, dígitos y puntuación.")]
        public required string Password { get; set; }
        [JsonIgnore]
        [NotMapped]
        public required string PasswordPlano { get; set; }
        public override void EsValido()
        {
            base.EsValido(); // Llama a la validación de Usuario
            if (string.IsNullOrWhiteSpace(Email) ||
                !Regex.IsMatch(Email, @"^[^\s@]+@[^\s@]+\.[^\s@]+$"))
                throw new UsuarioException("El email no tiene un formato válido.");

            // ✅ Validar la contraseña original (plaintext)
            if (string.IsNullOrWhiteSpace(PasswordPlano) || PasswordPlano.Length < 6 ||
                !Regex.IsMatch(PasswordPlano, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[.,;!?])[A-Za-z\d.,;!?]+$"))
            {
                throw new UsuarioException("La contraseña debe tener al menos una letra mayúscula, una minúscula, un número y un signo de puntuación (.,;!?).");
            }
        }
    }
}

