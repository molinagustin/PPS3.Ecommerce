using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EstadosCarrosComprasController : ControllerBase
    {
        private readonly IRepEstadoCarroCompra _repEstadoCarroCompra;

        public EstadosCarrosComprasController(IRepEstadoCarroCompra repEstadoCarroCompra) => _repEstadoCarroCompra = repEstadoCarroCompra;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoCarroCompra>>> ObtenerEstados()
        {
            var response = await _repEstadoCarroCompra.ObtenerEstados();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoCarroCompra>> ObtenerEstado(int id)
        {
            var response = await _repEstadoCarroCompra.ObtenerEstado(id);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }
    }
}
