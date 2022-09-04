using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IServEmail _servEmail;

        public EmailController(IServEmail servEmail)
        {
            _servEmail = servEmail;
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult EmailContacto([FromBody] EmailBasico datosEmail)
        {
            var emailEnviado = _servEmail.EmailContacto(datosEmail);
            if (emailEnviado)
                return Ok();
            else
                return BadRequest();
        }

        [HttpPost]
        public ActionResult EmailVerificacion([FromBody] EmailAutenticacion datosEmail)
        {
            var emailEnviado = _servEmail.EmailVerificacion(datosEmail);
            if (emailEnviado)
                return Ok();
            else
                return BadRequest();
        }
    }
}
