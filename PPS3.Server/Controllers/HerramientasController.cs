using Microsoft.AspNetCore.Mvc;
using PPS3.Shared.Models;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HerramientasController : ControllerBase
    {
        private readonly IRepHerramientas _repHerramientas;

        public HerramientasController(IRepHerramientas repHerramientas) => _repHerramientas = repHerramientas;

        [HttpPost]
        public async Task<ActionResult<int>> CambiarPrecios([FromBody] CambioPrecios cambios)
        {
            var response = await _repHerramientas.CambiarPrecios(cambios);

            if (response > 0) return Ok(response);
            else return BadRequest();
        }
    }
}
