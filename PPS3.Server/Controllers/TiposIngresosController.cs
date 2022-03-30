using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TiposIngresosController : ControllerBase
    {
        private readonly IRepTipoIngreso _repTipoIngreso;

        public TiposIngresosController(IRepTipoIngreso repTipoIngreso) => _repTipoIngreso = repTipoIngreso;
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoIngreso>>> ObtenerTiposIngresos()
        {
            var response = await _repTipoIngreso.ObtenerTiposIngresos();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TipoIngreso>> ObtenerTipoIngreso(int id)
        {
            var response = await _repTipoIngreso.ObtenerTipoIngreso(id);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }
    }
}
