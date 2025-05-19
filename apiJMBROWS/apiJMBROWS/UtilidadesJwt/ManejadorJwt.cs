using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace apiJMBROWS.UtilidadesJwt
{
    public class ManejadorJwt
    {

        /// <summary>
        /// Método para generar el token JWT usando una función estática (no es necesario tener instancias)
        /// </summary>

        /// <remarks> Creación del "payload" con tiene la información del usuario que se logueó (subject)
        /// El usuario tiene "claims", que son pares nombre/valor que se utilizan para guardar
        /// en el cliente. No pueden ser sensibles
        /// Se le debe setear el periodo temporal de validez (Expires)
        ///Se utiliza un algoritmo de clave simétrica para generar el token</remarks>

        public static string GenerarToken(string email, string rol, string key, string issuer, string audience)
        {
            var claims = new[]
            {
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Role, rol)
        };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
