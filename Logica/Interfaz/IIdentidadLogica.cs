using Dominio.Modelo;
using System.IdentityModel.Tokens.Jwt;

namespace Logica.Interfaz
{
    public interface IIdentidadLogica
    {
        string Login(LoginModelo login);
    }
}
