using Microsoft.AspNetCore.Mvc;
using PPS3.Shared.InternalModels;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DetallesRecibosController : ControllerBase
    {
        private readonly IRepDetalleRecibo _repDetRec;

        public DetallesRecibosController(IRepDetalleRecibo repDetRec) => _repDetRec = repDetRec;
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleRecibo>>> ObtenerDetallesPorComprobante(int idComprobante)
        {
            if (idComprobante <= 0)
                return BadRequest();

            var response = await _repDetRec.ObtenerDetallesPorComprobante(idComprobante);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleRecibo>>> ObtenerDetallesRecibo(int idRecibo)
        {
            if (idRecibo <= 0)
                return BadRequest();

            var response = await _repDetRec.ObtenerDetallesRecibo(idRecibo);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleRecibo>>> ObtenerDetallesRecibos()
{
            var response = await _repDetRec.ObtenerDetallesRecibos();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> CrearDetalleRecibo([FromBody] DetalleRecibo detalleRec)
        {
            if (detalleRec == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repDetRec.InsertarDetalle(detalleRec);
                if (response != false)
                    return Ok(response);
                else
                    return BadRequest();
            }
            else
                return Problem();
        }
    }
}
