using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class IngresosCuentasCorrientesController : ControllerBase
    {
        private readonly IRepIngresoCuentaCorriente _repIngresoCC;

        public IngresosCuentasCorrientesController(IRepIngresoCuentaCorriente repIngresoCC) => _repIngresoCC = repIngresoCC;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngresoCuentaCorriente>>> ObtenerIngresosCC()
        {
            var response = await _repIngresoCC.ObtenerIngresosCC();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IngresoCuentaCorriente>> ObtenerIngresoCC(int id)
        {
            var response = await _repIngresoCC.ObtenerIngresoCC(id);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult> CrearIngresoCC([FromBody] IngresoCuentaCorriente ingresoCC)
        {
            if (ingresoCC == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repIngresoCC.InsertarIngresoCC(ingresoCC);
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
