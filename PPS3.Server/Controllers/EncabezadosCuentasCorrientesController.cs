using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EncabezadosCuentasCorrientesController : ControllerBase
    {
        private readonly IRepEncabezadoCuentaCorriente _repEncabCC;

        public EncabezadosCuentasCorrientesController(IRepEncabezadoCuentaCorriente repEncabCC) => _repEncabCC = repEncabCC;
         
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EncabezadoCuentaCorriente>>> ObtenerCuentasCorrientes()
        {
            var response = await _repEncabCC.ObtenerCuentasCorrientes();
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CuentasCorrientesListado>>> ObtenerCCListado()
        {
            var response = await _repEncabCC.ObtenerCCListado();
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<EncabezadoCuentaCorriente>> ObtenerCuentaCorriente(int numCC)
        {
            var response = await _repEncabCC.ObtenerCuentaCorriente(numCC);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<EncabezadoCuentaCorriente>> ObtenerCCCliente(int idCliente)
        {
            var response = await _repEncabCC.ObtenerCCCliente(idCliente);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult> CrearCuentaCorriente([FromBody] EncabezadoCuentaCorriente encabezadoCC)
        {
            if (encabezadoCC == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repEncabCC.InsertarCuentaCorriente(encabezadoCC);
                if (response != false)
                    return Ok();
                else
                    return BadRequest();
            }
            else
                return Problem();
        }

        [HttpPut]
        public async Task<ActionResult> ActualizarCuentaCorriente([FromBody] EncabezadoCuentaCorriente encabezadoCC)
        {
            if (encabezadoCC == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repEncabCC.ActualizarCuentaCorriente(encabezadoCC);
                if (response != false)
                    return Ok();
                else
                    return BadRequest();
            }
            else
                return Problem();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> BorrarCuentaCorriente(int id)
{
            var response = await _repEncabCC.BorrarCuentaCorriente(id);
            if (response != false)
                return Ok();
            else
                return BadRequest();
        }
    }
}
