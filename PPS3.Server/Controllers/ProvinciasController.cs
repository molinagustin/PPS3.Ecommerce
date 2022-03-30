using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinciasController : ControllerBase
    {
        private readonly IRepProvincia _repProvincia;

        public ProvinciasController(IRepProvincia repProvincia) => _repProvincia = repProvincia;

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Provincia>>> ObtenerProvincias()
        {
            var response = await _repProvincia.ObtenerProvincias();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Provincia>> ObtenerProvincia(int id)
        {
            var response = await _repProvincia.ObtenerProvincia(id);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("{nombreProvincia}")]
        public async Task<ActionResult<Provincia>> ObtenerProvincia(string nombreProvincia)
        {
            var response = await _repProvincia.ObtenerProvincia(nombreProvincia);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }
    }
}
