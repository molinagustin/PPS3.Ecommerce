using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EncabezadosComprobantesController : ControllerBase
    {
        private readonly IRepEncabezadoComprobante _repEncabComp;

        public EncabezadosComprobantesController(IRepEncabezadoComprobante repEncabComp) => _repEncabComp = repEncabComp;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EncabezadoComprobante>>> ObtenerEncabezados()
        {
            var response = await _repEncabComp.ObtenerEncabezadosComp();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EncabezadoComprobante>> ObtenerEncabezado(int id)
        {
            var response = await _repEncabComp.ObtenerEncabezadoComp(id);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult<int>> CrearEncabezadoComp([FromBody] EncabezadoComprobante encabezadoComp)
        {
            if (encabezadoComp == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repEncabComp.InsertarEncabezadoComp(encabezadoComp);
                //Devuelvo el ID del comprobante ingresado
                if (response > 0)
                    return Ok(response);
                else
                    return BadRequest();
            }
            else
                return Problem();
        }
    }
}
