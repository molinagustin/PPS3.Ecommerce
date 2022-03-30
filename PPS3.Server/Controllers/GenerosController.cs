using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class GenerosController : ControllerBase
    {
        private readonly IRepGenero _repGenero;

        public GenerosController(IRepGenero repGenero) => _repGenero = repGenero;

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Genero>>> ObtenerGeneros()
        {
            var response = await _repGenero.ObtenerGeneros();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Genero>> ObtenerGenero(int id)
        {
            var response = await _repGenero.ObtenerGenero(id);
            
            if(response != null)
                return Ok(response);
            else
                return BadRequest();
        }
    }
}