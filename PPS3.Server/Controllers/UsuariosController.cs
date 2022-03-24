using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IRepUsuario _repUsuario;
        private readonly IRepCliente _repCliente;

        public UsuariosController(IRepUsuario repUsuario, IRepCliente repCliente)
        {
            _repUsuario = repUsuario;
            _repCliente = repCliente;
        } 

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
        public async Task<ActionResult> CrearUsuario([FromBody] UsuarioCliente usuarioCliente)
        {
            if (usuarioCliente == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                //Luego de la verificacion del modelo valido, creo un nuevo cliente y traigo su id, el cual, si es mayor a 0 se lo asigno al usuario que estoy creando
                var clienteResponse = await _repCliente.CrearCliente(usuarioCliente);
                if (clienteResponse > 0)
                {
                    usuarioCliente.IdCliente = clienteResponse;
                    var response = await _repUsuario.CrearUsuario(usuarioCliente);
                    if (response != false)
                        return Ok();
                    else
                        return BadRequest();
                }
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