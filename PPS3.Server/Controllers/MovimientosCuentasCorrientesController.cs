using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MovimientosCuentasCorrientesController : ControllerBase
    {
        private readonly IRepMovimientoCuentaCorriente _repMovimientoCC;

        public MovimientosCuentasCorrientesController(IRepMovimientoCuentaCorriente repMovimientoCC) => _repMovimientoCC = repMovimientoCC;
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovimientoCuentaCorriente>>> ObtenerMovimientos()
        {
            var response = await _repMovimientoCC.ObtenerMovimientos();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MovimientoCuentaCorriente>> ObtenerMovimiento(int id)
        {
            var response = await _repMovimientoCC.ObtenerMovimiento(id);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<int>> CrearMovimientoCC([FromBody] MovimientoCuentaCorriente movimientoCC)
        {
            if (movimientoCC == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repMovimientoCC.InsertarMovimiento(movimientoCC);
                //Se el response es mayor a 0, quiere decir que se creo y obtuvo el ID de forma exitosa, asi que lo devuelvo
                if (response > 0)
                    return Ok(response);
                else
                    return BadRequest();
            }
            else
                return Problem();
        }
    }
}
