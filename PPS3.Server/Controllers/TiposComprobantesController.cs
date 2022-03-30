using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TiposComprobantesController : ControllerBase
    {
        private readonly IRepTipoComprobante _repTipoComprobante;

        public TiposComprobantesController(IRepTipoComprobante repTipoComprobante) => _repTipoComprobante = repTipoComprobante;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoComprobante>>> ObtenerTiposComprobantes()
        {
            var response = await _repTipoComprobante.ObtenerTiposComprobantes();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoComprobante>> ObtenerTipoComprobante(int id)
        {
            var response = await _repTipoComprobante.ObtenerTipoComprobante(id);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }
    }
}