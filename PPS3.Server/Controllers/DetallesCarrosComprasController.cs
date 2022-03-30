using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DetallesCarrosComprasController : ControllerBase
    {
        private readonly IRepDetalleCarroCompra _repDetalleCarro;

        public DetallesCarrosComprasController(IRepDetalleCarroCompra repDetalleCarro) => _repDetalleCarro = repDetalleCarro;
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleCarroCompra>>> ObtenerTodosDetalles()
        {
            var response = await _repDetalleCarro.ObtenerTodosDetalles();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DetalleCarroCompra>> ObtenerDetalle(int id)
        {
            var response = await _repDetalleCarro.ObtenerDetalle(id);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleCarroCompra>>> ObtenerDetallesCarro(int idCarro)
        {
            var response = await _repDetalleCarro.ObtenerDetallesCarro(idCarro);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult> CrearDetalleCarro([FromBody] DetalleCarroCompra detalleCarroCompra)
        {
            if (detalleCarroCompra == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repDetalleCarro.InsertarDetalleCarro(detalleCarroCompra);
                if (response != false)
                    return Ok();
                else
                    return BadRequest();
            }
            else
                return Problem();
        }

        [HttpPut]
        public async Task<ActionResult> ActualizarDetalleCarro([FromBody] DetalleCarroCompra detalleCarroCompra)
        {
            if (detalleCarroCompra == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repDetalleCarro.ActualizarDetalleCarro(detalleCarroCompra);
                if (response != false)
                    return Ok();
                else
                    return BadRequest();
            }
            else
                return Problem();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> BorrarDetalleCarro(int id)
        {
            var response = await _repDetalleCarro.BorrarDetalleCarro(id);
            if (response != false)
                return Ok();
            else
                return BadRequest();
        }
    }
}
