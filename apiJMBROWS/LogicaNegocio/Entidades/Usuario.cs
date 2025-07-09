using Libreria.LogicaNegocio.Excepciones;
using LogicaNegocio.InterfacesEntidades;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace LogicaNegocio.Entidades
{
    public abstract class Usuario : IValidable, IEquatable<Usuario>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public required string Nombre { get; set; }
        [Required]
        public required string Apellido { get; set; }

        public abstract string Rol { get; }

        public bool Equals(Usuario? other)
        {
            return other != null && Id == other.Id;
        }

        public virtual void EsValido()
        {

            if (string.IsNullOrWhiteSpace(Nombre) || string.IsNullOrWhiteSpace(Apellido))
                throw new UsuarioException("El nombre y el apellido no pueden estar vacíos.");

            if (!Regex.IsMatch(Nombre, @"^[a-zA-ZáéíóúÁÉÍÓÚ\s'-]+$") ||
                !Regex.IsMatch(Apellido, @"^[a-zA-ZáéíóúÁÉÍÓÚ\s'-]+$"))
                throw new UsuarioException("Nombre y apellido solo pueden contener letras, espacios, guiones o apóstrofes.");

           
        }



    }
}
