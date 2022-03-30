using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TiposTarjetasController : ControllerBase
    {
        private readonly IRepTipoTarjeta _repTipoTarjeta;

        public TiposTarjetasController(IRepTipoTarjeta repTipoTarjeta) => _repTipoTarjeta = repTipoTarjeta;
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoTarjeta>>> ObtenerTiposTarjetas()
        {
            var response = await _repTipoTarjeta.ObtenerTiposTarjetas();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoTarjeta>> ObtenerTipoTarjeta(int id)
        {
            var response = await _repTipoTarjeta.ObtenerTipoTarjeta(id);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }
    }
}
