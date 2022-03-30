using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TiposClientesController : ControllerBase
    {
        private readonly IRepTipoCliente _repTipoCliente;

        public TiposClientesController(IRepTipoCliente repTipoCliente) => _repTipoCliente = repTipoCliente;

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoCliente>>> ObtenerTiposClientes()
        {
            var response = await _repTipoCliente.ObtenerTiposClientes();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("id")]
        public async Task<ActionResult<TipoCliente>> ObtenerTipoCliente(int id)
        {
            var response = await _repTipoCliente.ObtenerTipoCliente(id);

            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }
    }
}