using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TiposProductosController : ControllerBase
    {
        private readonly IRepTipoProducto _repTipoProducto;

        public TiposProductosController(IRepTipoProducto repTipoProducto) => _repTipoProducto = repTipoProducto;

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoProducto>>> ObtenerTiposProductos()
        {
            var response = await _repTipoProducto.ObtenerTiposProductos();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoProducto>> ObtenerTipoProducto(int id)
        {
            var response = await _repTipoProducto.ObtenerTipoProducto(id);
            if(response != null)
                return Ok(response);
            else
                return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TiposProductosListado>>> ObtenerTiposProdList()
        {
            var response = await _repTipoProducto.ObtenerTiposProdList();
            return Ok(response);
        }

        [HttpGet("{idTipo}")]
        public async Task<ActionResult<int>> CantidadProductosActivos(int idTipo)
        {
            var response = await _repTipoProducto.CantidadProductosActivos(idTipo);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> CrearTipoProducto([FromBody] TipoProducto tipoProducto)
        {
            if (tipoProducto == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repTipoProducto.InsertarTipoProducto(tipoProducto);
                if (response != false)
                    return Ok();
                else
                    return BadRequest();
            }
            else
                return Problem();
        }

        [HttpPut]
        public async Task<ActionResult> ActualizarTipoProducto([FromBody]TipoProducto tipoProducto)
        {
            if (tipoProducto == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repTipoProducto.ActualizarTipoProducto(tipoProducto);
                if (response != false)
                    return Ok();
                else
                    return BadRequest();
            }
            else
                return Problem();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> BorrarTipoProducto(int id)
        {
            var response = await _repTipoProducto.BorrarTipoProducto(id);
            if (response != false)
                return Ok();
            else
                return BadRequest();
        }
    }
}
