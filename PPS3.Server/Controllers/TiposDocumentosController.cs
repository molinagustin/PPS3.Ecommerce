using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TiposDocumentosController : ControllerBase
    {
        private readonly IRepTipoDocumento _repTipoDocumento;

        public TiposDocumentosController(IRepTipoDocumento repTipoDocumento) => _repTipoDocumento = repTipoDocumento;

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoDocumento>>> ObtenerTiposDocumentos()
        {
            var response = await _repTipoDocumento.ObtenerTiposDocumentos();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoDocumento>> ObtenerTipoDocumento(int id)
        {
            var response = await _repTipoDocumento.ObtenerTipoDocumento(id);
            
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }
    }
}
