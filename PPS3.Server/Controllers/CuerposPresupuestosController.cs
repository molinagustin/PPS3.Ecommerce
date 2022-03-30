using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CuerposPresupuestosController : ControllerBase
    {
        private readonly IRepCuerpoPresupuesto _repCuerpoPresupuesto;

        public CuerposPresupuestosController(IRepCuerpoPresupuesto repCuerpoPresupuesto) => _repCuerpoPresupuesto = repCuerpoPresupuesto;
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CuerpoPresupuesto>>> ObtenerCuerposDePresupuesto(int numPresupuesto)
        {
            var response = await _repCuerpoPresupuesto.ObtenerCuerposDePresupuesto(numPresupuesto);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<CuerpoPresupuesto>> ObtenerCuerpoPresupuesto(int idCuerpo)
        {
            var response = await _repCuerpoPresupuesto.ObtenerCuerpoPresupuesto(idCuerpo);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult> CrearCuerpoPresupuesto([FromBody] CuerpoPresupuesto cuerpoPresupuesto)
        {
            if (cuerpoPresupuesto == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repCuerpoPresupuesto.InsertarNuevoCuerpo(cuerpoPresupuesto);
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
