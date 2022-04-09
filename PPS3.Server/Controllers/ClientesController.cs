using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IRepCliente _repCliente;

        public ClientesController(IRepCliente repCliente) => _repCliente = repCliente;
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> ObtenerClientes()
        {
            var response = await _repCliente.ObtenerClientes();
            return Ok(response);
        }

        [HttpGet("id:int")]
        public async Task<ActionResult<Cliente>> ObtenerCliente(int id)
        {
            var response = await _repCliente.ObtenerCliente(id);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }

        [HttpGet("nombreCliente")]
        public async Task<ActionResult<Cliente>> ObtenerCliente(string nombreCliente)
        {
            var response = await _repCliente.ObtenerCliente(nombreCliente);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }
    }
}
