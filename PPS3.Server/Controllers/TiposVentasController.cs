using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TiposVentasController : ControllerBase
    {
        private readonly IRepTipoVenta _repTipoVenta;

        public TiposVentasController(IRepTipoVenta repTipoVenta) => _repTipoVenta = repTipoVenta;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoVenta>>> ObtenerTiposVentas()
        {
            var response = await _repTipoVenta.ObtenerTiposVentas();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoVenta>> ObtenerTipoVenta(int id)
        {
            var response = await _repTipoVenta.ObtenerTipoVenta(id);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }
    }
}
