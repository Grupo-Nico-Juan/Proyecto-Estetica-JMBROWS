
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace apiJMBROWS.UtilidadesJwt
{
    public static class ManejadorJwt
    {
        /// <summary>
        /// Genera un token JWT con email + rol.
        /// </summary>
        public static string GenerarToken(
            string email,
            string tipoUsuario,
            JwtSettings cfg)                           // ⬅ nuevo parámetro
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role,  tipoUsuario)
            };

            var securityKey = new SymmetricSecurityKey(
                               Encoding.UTF8.GetBytes(cfg.Key));
            var creds = new SigningCredentials(
                               securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: cfg.Issuer,
                audience: cfg.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Genera un token JWT para cliente (solo email).
        /// </summary>
        public static string GenerarTokenCliente(
            string email,
            JwtSettings cfg)                           // ⬅ nuevo parámetro
        {
            var claims = new[] { new Claim("email", email) };

            var key = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(cfg.Key));
            var creds = new SigningCredentials(
                        key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: cfg.Issuer,
                audience: cfg.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // ───── (opcional) overload antiguo para compatibilidad ─────

    }
}
