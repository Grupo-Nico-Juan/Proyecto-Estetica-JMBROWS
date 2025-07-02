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
        [Phone]
        [MaxLength(20)]
        public required string Telefono { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        public string? Password { get; set; }

        [Required]
        public required string Nombre { get; set; }

        [Required]
        public required string Apellido { get; set; }

        [JsonIgnore]
        [NotMapped]
        public string? PasswordPlano { get; set; } = string.Empty;
        public bool EsRegistrado { get; set; } // true si tiene cuenta con contraseña, false si es ocasional

        public bool TelefonoVerificado { get; set; } = false;
        public List<Turno> Turnos { get; set; } = new();
        public List<Promocion> Promociones { get; set; } = new();
        public List<Notificacion> Notificaciones { get; set; } = new();

        public bool Equals(Cliente? other)
        {
            return other != null && Telefono == other.Telefono;
        }

        public virtual void EsValido()
        {
            // Teléfono Uruguay: +598XXXXXXXX o 09XXXXXXX
            bool esTelInt = Regex.IsMatch(Telefono, @"^\+598[1-9]\d{7}$");
            bool esTelNac = Regex.IsMatch(Telefono, @"^09\d{7}$");
            if (!esTelInt && !esTelNac)
                throw new UsuarioException("El teléfono debe ser uruguayo y tener formato +598XXXXXXXX o 09XXXXXXX.");

            if (!string.IsNullOrWhiteSpace(Email) &&
                !Regex.IsMatch(Email, @"^[^\s@]+@[^\s@]+\.[^\s@]+$"))
                throw new UsuarioException("El email no tiene un formato válido.");

            if (string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(Apellido))
                throw new UsuarioException("El nombre y el apellido no pueden estar vacíos.");

            if (!Regex.IsMatch(Nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚ\s'-]+$") ||
                !Regex.IsMatch(Apellido, @"^[a-zA-ZáéíóúÁÉÍÓÚ\s'-]+$"))
                throw new UsuarioException("Nombre y apellido solo pueden contener letras, espacios, guiones o apóstrofes.");

            // Solo valida la password si se provee
            if (!string.IsNullOrWhiteSpace(PasswordPlano) && PasswordPlano.Length < 6 ||
                (!string.IsNullOrWhiteSpace(PasswordPlano) &&
                !Regex.IsMatch(PasswordPlano, @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[.,;!?])[A-Za-z\d.,;!?]+$")))
            {
                throw new UsuarioException("La contraseña debe tener al menos una letra mayúscula, una minúscula, un número y un signo de puntuación (.,;!?).");
            }
        }
    }
}


