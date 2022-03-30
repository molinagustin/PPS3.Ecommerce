using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ConceptosComprobantesController : ControllerBase
    {
        private readonly IRepConceptoComprobante _repConceptoComp;

        public ConceptosComprobantesController(IRepConceptoComprobante repConceptoComp) => _repConceptoComp = repConceptoComp;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ConceptoComprobante>>> ObtenerConceptosComprobantes()
        {
            var response = await _repConceptoComp.ObtenerConceptosComprobantes();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ConceptoComprobante>> ObtenerConceptoComprobante(int id)
        {
            var response = await _repConceptoComp.ObtenerConceptoComprobante(id);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }
    }
}
