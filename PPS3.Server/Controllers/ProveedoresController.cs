using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {
        private readonly IRepProveedor _repProveedor;

        public ProveedoresController(IRepProveedor repProveedor) => _repProveedor = repProveedor;

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proveedor>>> ObtenerProveedores()
        {
            var response = await _repProveedor.ObtenerProveedores();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Proveedor>> ObtenerProveedor(int id)
        {
            var response = await _repProveedor.ObtenerProveedor(id);
           
            if (response != null)
                return Ok(response);            
            else
                return NotFound(); //Code 404
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProveedorListado>>> ObtenerProveedoresListado()
        {
            var response = await _repProveedor.ObtenerProveedoresListado();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CrearProveedor([FromBody] Proveedor proveedor)
        {
            if (proveedor == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repProveedor.InsertarProveedor(proveedor);
                if (response > 0)
                    return Ok(response);
                else
                    return BadRequest();
            }
            else
                return Problem(); //Un response que se puede personalizar
        }

        [HttpPut]
        public async Task<ActionResult<int>> ActualizarProveedor([FromBody] Proveedor proveedor)
        {
            if (proveedor == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repProveedor.ActualizarProveedor(proveedor);
                if (response > 0)
                    return Ok(response);
                else
                    return BadRequest();
            }
            else
                return Problem(); //Un response que se puede personalizar
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> BorrarProveedor(int id)
        {
            var response = await _repProveedor.BorrarProveedor(id);

            if (response = !false)
                return Ok();
            else
                return BadRequest();
        }
    }
}
