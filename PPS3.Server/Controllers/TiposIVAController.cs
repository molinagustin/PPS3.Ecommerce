using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TiposIVAController : ControllerBase
    {
        private readonly IRepTipoIVA _repTipoIVA;

        public TiposIVAController(IRepTipoIVA repTipoIVA) => _repTipoIVA = repTipoIVA;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoIVA>>> ObtenerTiposIVAs()
        {
            var response = await _repTipoIVA.ObtenerTiposIVAs();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoIVA>> ObtenerTipoIVA(int id)
        {
            var response = await _repTipoIVA.ObtenerTipoIVA(id);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }
    }
}
