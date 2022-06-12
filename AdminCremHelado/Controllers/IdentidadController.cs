using Dominio.Modelo;
using Logica.Interfaz;
using Microsoft.AspNetCore.Mvc;

namespace AdminCremHelado.Controllers
{
    [Route("api/identidad")]
    [ApiController]
    public class IdentidadController : ControllerBase
    {
        private readonly IIdentidadLogica _identidad;
        public IdentidadController(IIdentidadLogica identidad)
        {
            _identidad = identidad;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginModelo login)
        {
            return Ok(_identidad.Login(login));
        }
    }
}
