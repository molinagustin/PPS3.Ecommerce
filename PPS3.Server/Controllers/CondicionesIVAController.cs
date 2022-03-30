using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CondicionesIVAController : ControllerBase
    {
        private readonly IRepCondicionIVA _repCondicionIVA;

        public CondicionesIVAController(IRepCondicionIVA repCondicionIVA) => _repCondicionIVA = repCondicionIVA;

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CondicionIVA>>> ObtenerCondicionesIVA()
        {
            var response = await _repCondicionIVA.ObtenerCondicionesIVA();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<CondicionIVA>> ObtenerCondicionIVA(int id)
        {
            var response = await _repCondicionIVA.ObtenerCondicionIVA(id);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }
    }
}
