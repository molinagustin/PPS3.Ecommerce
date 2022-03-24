using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProvinciasController : ControllerBase
    {
        private readonly IRepProvincia _repProvincia;

        public ProvinciasController(IRepProvincia repProvincia) => _repProvincia = repProvincia;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Provincia>>> ObtenerProvincias()
        {
            var response = await _repProvincia.ObtenerProvincias();
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Provincia>> ObtenerProvincia(int id)
        {
            var response = await _repProvincia.ObtenerProvincia(id);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }

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
