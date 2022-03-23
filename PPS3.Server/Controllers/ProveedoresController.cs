using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {
        private readonly IRepProveedor _repProveedor;

        public ProveedoresController(IRepProveedor repProveedor) => _repProveedor = repProveedor;
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Proveedor>>> ObtenerProveedores()
        {
            var response = await _repProveedor.ObtenerProveedores();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Proveedor>> ObtenerProveedor(int id)
        {
            var response = await _repProveedor.ObtenerProveedor(id);
           
            if (response != null)
                return Ok(response);            
            else
                return NotFound(); //Code 404
        }

        [HttpPost]
        public async Task<ActionResult> CrearProveedor([FromBody] Proveedor proveedor)
        {
            if (proveedor == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repProveedor.InsertarProveedor(proveedor);
                if (response != false)
                    return Ok();
                else
                    return BadRequest();
            }
            else
                return Problem(); //Un response que se puede personalizar
        }

        [HttpPut]
        public async Task<ActionResult> ActualizarProveedor([FromBody] Proveedor proveedor)
        {
            if (proveedor == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repProveedor.ActualizarProveedor(proveedor);
                if (response != false)
                    return Ok();
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
