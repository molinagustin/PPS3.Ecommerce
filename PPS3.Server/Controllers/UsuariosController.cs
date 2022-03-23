using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IRepUsuario _repUsuario;

        public UsuariosController(IRepUsuario repUsuario) => _repUsuario = repUsuario;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> ObtenerUsuarios()
        {
            var response = await _repUsuario.ObtenerUsuarios();
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Usuario>> ObtenerUsuario(int id)
        {
            var response = await _repUsuario.ObtenerUsuario(id);
            if (response != null)
                return Ok(response);
            else
                return NotFound();            
        }

        [HttpGet("{nombreUsuario}")]
        public async Task<ActionResult<Usuario>> ObtenerUsuario(string nombreUsuario)
        {
            var response = await _repUsuario.ObtenerUsuario(nombreUsuario);
            if (response != null)
                return Ok(response);
            else
                return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult> CrearUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repUsuario.CrearUsuario(usuario);
                if (response != false)
                    return Ok();
                else
                    return BadRequest();
            }
            else
                return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> ActualizarUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repUsuario.ActualizarUsuario(usuario);
                if (response != false)
                    return Ok();
                else
                    return BadRequest();
            }
            else
                return Problem();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> BorrarUsuario(int id)
        {
            var response = await _repUsuario.BorrarUsuario(id);
            if (response != false)
                return Ok();
            else
                return BadRequest();
        }
    }
}