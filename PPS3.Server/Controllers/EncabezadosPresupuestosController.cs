using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EncabezadosPresupuestosController : ControllerBase
    {
        private readonly IRepEncabezadoPresupuesto _repEncab;

        public EncabezadosPresupuestosController(IRepEncabezadoPresupuesto repEncab) => _repEncab = repEncab;
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EncabezadoPresupuesto>>> ObtenerEncabezados()
        {
            var response = await _repEncab.ObtenerTodosPresupuestos();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EncabezadoPresupuesto>> ObtenerEncabezado(int id)
        {
            var response = await _repEncab.ObtenerPresupuesto(id);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult> CrearEncabezado([FromBody] EncabezadoPresupuesto encabezadoPresupuesto)
        { 
            if (encabezadoPresupuesto == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repEncab.InsertarPresupuesto(encabezadoPresupuesto);
                if (response != false)
                    return Ok();
                else
                    return BadRequest();
            }
            else
                return BadRequest();
        }
    }
}
