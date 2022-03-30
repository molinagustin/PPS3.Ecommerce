using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PuntosVentasController : ControllerBase
    {
        private readonly IRepPuntoVenta _repPuntoVenta;

        public PuntosVentasController(IRepPuntoVenta repPuntoVenta) => _repPuntoVenta = repPuntoVenta;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PuntoVenta>>> ObtenerPuntosVentas()
        {
            var response = await _repPuntoVenta.ObtenerPuntosVentas();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PuntoVenta>> ObtenerPuntoVenta(int id)
        {
            var response = await _repPuntoVenta.ObtenerPuntoVenta(id);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }
    }
}
