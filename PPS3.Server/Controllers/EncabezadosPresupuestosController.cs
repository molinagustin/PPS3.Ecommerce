using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Presupuesto>>> ObtenerPresupuestosList()
        {
            var response = await _repEncab.ObtenerPresupuestosList();
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetallePresupuesto>>> ObtenerDetallesPresupuestosList()
        {
            var response = await _repEncab.ObtenerDetallesPresupuestosList();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CrearEncabezado([FromBody] EncabezadoPresupuesto encabezadoPresupuesto)
        { 
            if (encabezadoPresupuesto == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repEncab.InsertarPresupuesto(encabezadoPresupuesto);
                if (response > 0)
                    return Ok(response);
                else
                    return BadRequest();
            }
            else
                return BadRequest();
        }
    }
}
