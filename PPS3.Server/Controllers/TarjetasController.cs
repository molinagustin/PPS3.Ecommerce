using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetasController : ControllerBase
    {
        private readonly IRepTarjeta _repTarjeta;

        public TarjetasController(IRepTarjeta repTarjeta) => _repTarjeta = repTarjeta;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarjeta>>> ObtenerTarjetas()
        {
            var response = await _repTarjeta.ObtenerTarjetas();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tarjeta>> ObtenerTarjeta(int id)
        {
            var response = await _repTarjeta.ObtenerTarjeta(id);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }
    }
}
