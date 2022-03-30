using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FormasPagoController : ControllerBase
    {
        private readonly IRepFormaPago _repFormaPago;

        public FormasPagoController(IRepFormaPago repFormaPago) => _repFormaPago = repFormaPago;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormaPago>>> ObtenerFormasPago()
        {
            var response = await _repFormaPago.ObtenerFormasPago();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FormaPago>> ObtenerFormaPago(int id)
        {
            var response = await _repFormaPago.ObtenerFormaPago(id);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }
    }
}
