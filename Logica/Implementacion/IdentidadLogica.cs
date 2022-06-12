using Dominio.Modelo;
using Logica.Interfaz;
using Logica.Utilidades;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Transversal;

namespace Logica
{
    public class IdentidadLogica : IIdentidadLogica
    {
        private readonly IConfiguration _configuration;

        public IdentidadLogica(IConfiguration configuration, IIdentidadLogica identidad)
        {
            _configuration = configuration;
        }
        public string Login(LoginModelo login) 
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                if (login.Usuario != "kmillan" || login.Contraseña != "123")
                    return "Usuario y/o contraseña incorrectos" ;

                JwtSecurityToken token = GenerarToken(login);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception e)
            {
                Logger.EscribirLogger("Error al intentar logearse, error: "+e.Message);
                return "Error al intentar logearse, error: "+e.Message;
            }
        }

        private JwtSecurityToken GenerarToken(LoginModelo login) 
        {
            string ValidarIssuer = _configuration["ApiAuth:Issuer"];
            string ValidarAudience = _configuration["ApiAuth:Audience"];
            SymmetricSecurityKey IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["ApiAuth:SecretKey"]));

            //La fecha de expiracion sera el mismo dia a las 12 de la noche
            DateTime dtFechaExpiraToken;
            DateTime now = DateTime.Now;
            dtFechaExpiraToken = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59, 999);

            //Agregamos los claim nuestros
            var claims = new[]
            {
                new Claim(Constantes.JWT_CLAIM_USUARIO, login.Usuario)
            };

            return new JwtSecurityToken
            (
                issuer: ValidarIssuer,
                audience: ValidarAudience,
                claims: claims,
                expires: dtFechaExpiraToken,
                notBefore: now,
                signingCredentials: new SigningCredentials(IssuerSigningKey, SecurityAlgorithms.HmacSha256)
            );
        }
    }
}
