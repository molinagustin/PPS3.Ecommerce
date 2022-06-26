using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CarrosComprasController : ControllerBase
    {
        private readonly IRepCarroCompra _repCarroCompra;

        public CarrosComprasController(IRepCarroCompra repCarroCompra) => _repCarroCompra = repCarroCompra;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarroCompra>>> ObtenerCarrosCompras()
        {
            var response = await _repCarroCompra.ObtenerCarrosCompras();
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrdenesCompraListado>>> ObtenerOrdenesCompra()
        {
            var response = await _repCarroCompra.ObtenerOrdenesCompra();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CarroCompra>> ObtenerCarroCompra(int id)
        {
            var response = await _repCarroCompra.ObtenerCarroCompra(id);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }

        [HttpGet("{NumOrden}")]
        public async Task<ActionResult<OrdenesCompraListado>> ObtenerOCDetalle(int NumOrden)
        {
            var response = await _repCarroCompra.ObtenerOCDetalle(NumOrden);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }

        [HttpGet("{NumOrden}")]
        public async Task<ActionResult<IEnumerable<DetalleCarroCompra>>> ObtenerOCDetalles(int NumOrden)
        {
            var response = await _repCarroCompra.ObtenerOCDetalles(NumOrden);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<int>> CrearCarroCompra([FromBody] int idUsuario)
        {
            if (idUsuario < 1)
                return BadRequest();

            var response = await _repCarroCompra.InsertarCarroCompra(idUsuario);
            if (response > 0)
                return Ok(response);
            else
                return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> ActualizarCarroCompra([FromBody] CarroCompra carroCompra)
        {
            if (carroCompra == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repCarroCompra.ActualizarCarroCompra(carroCompra);
                if (response != false)
                    return Ok();
                else
                    return BadRequest();
            }
            else
                return Problem();
        }
    }
}
