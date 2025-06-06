using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace apiJMBROWS.UtilidadesJwt
{
    public class ManejadorJwt
    {
        /// <summary>
        /// Genera un token JWT con claims personalizados: email y tipoUsuario.
        /// </summary>
        public static string GenerarToken(string email, string tipoUsuario, string key, string issuer, string audience)
        {
            var claims = new[]
            {
                    new Claim(ClaimTypes.Email, email),
                    new Claim(ClaimTypes.Role, tipoUsuario)
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Genera un token JWT para clientes (sin rol, solo email).
        /// </summary>
        public static string GenerarTokenCliente(string email, string key, string issuer, string audience)
        {
            var claims = new[]
            {
                new Claim("email", email)
                // Más adelante podés agregar tipoUsuario = Cliente si querés unificar
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
