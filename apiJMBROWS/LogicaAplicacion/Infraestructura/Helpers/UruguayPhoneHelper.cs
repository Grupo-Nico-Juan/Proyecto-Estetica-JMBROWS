using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LogicaAplicacion.Infraestructura.Helpers
{
    public static class UruguayPhoneHelper
    {
        /// <summary>
        /// Convierte 09XXXXXXX → +598XXXXXXXX y valida el formato.
        /// Lanza <see cref="ArgumentException"/> si no es válido.
        /// </summary>
        public static string Normalizar(string telefono)
        {
            if (string.IsNullOrWhiteSpace(telefono))
                throw new ArgumentException("Teléfono vacío");

            // Quita espacios o guiones
            telefono = telefono.Replace(" ", "").Replace("-", "");

            // Si viene 09… lo convertimos
            if (Regex.IsMatch(telefono, @"^09\d{7}$"))
                telefono = Regex.Replace(telefono, @"^0", "+598");

            if (!Regex.IsMatch(telefono, @"^\+598[1-9]\d{7}$"))
                throw new ArgumentException("El teléfono debe ser uruguayo y tener formato +598XXXXXXXX.");

            return telefono;
        }
    }

}
