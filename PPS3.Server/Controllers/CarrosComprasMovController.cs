using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarrosComprasMovController : ControllerBase
    {
        private readonly IRepMovCarro _repMovCarro;

        public CarrosComprasMovController(IRepMovCarro repMovCarro) => _repMovCarro = repMovCarro;

        [HttpPost]
        public async Task<ActionResult> CrearMovimiento(MovimientoCarroCompra movimiento)
        {
            if (movimiento == null)
                return BadRequest();

            var response = await _repMovCarro.CrearMovimiento(movimiento);
            if (response)
                return Ok();
            else
                return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("{idOrden:int}")]
        public async Task<ActionResult<IEnumerable<HistorialMovimientoCarro>>> ObtenerHistorial(int idOrden)
        {
            if (idOrden > 0)
            {
                var response = await _repMovCarro.ObtenerHistorial(idOrden);
                if (response != null)
                    return Ok(response);
                else
                    return BadRequest();
            }
            else
                return BadRequest();
        }
    }
}