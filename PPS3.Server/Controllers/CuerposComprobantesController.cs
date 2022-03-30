using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CuerposComprobantesController : ControllerBase
    {
        private readonly IRepCuerpoComprobante _repCuerpoComp;

        public CuerposComprobantesController(IRepCuerpoComprobante repCuerpoComp) => _repCuerpoComp = repCuerpoComp;
        
        //Obtener todos los cuerpos de un comprobante de cabecera en particular
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CuerpoComprobante>>> ObtenerCuerposDeComprobante(int numCabeceraComp)
        {
            var response = await _repCuerpoComp.ObtenerCuerposDeComprobante(numCabeceraComp);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }

        //Obtener solo un cuerpo de comprobante en particular
        [HttpGet]
        public async Task<ActionResult<CuerpoComprobante>> ObtenerCuerpoComp(int numCuerpo)
        {
            var response = await _repCuerpoComp.ObtenerCuerpoComp(numCuerpo);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult> CrearCuerpoComp([FromBody] CuerpoComprobante cuerpoComp)
        {
            if (cuerpoComp == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repCuerpoComp.InsertarCuerpoComp(cuerpoComp);
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