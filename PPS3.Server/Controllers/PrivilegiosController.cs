using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PrivilegiosController : ControllerBase
    {
        private readonly IRepPrivilegio _repPrivilegio;

        public PrivilegiosController(IRepPrivilegio repPrivilegio) => _repPrivilegio = repPrivilegio;
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Privilegio>>> ObtenerPrivilegios()
        {
            var response = await _repPrivilegio.ObtenerPrivilegios();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Privilegio>> ObtenerPrivilegio(int id)
        {
            var response = await _repPrivilegio.ObtenerPrivilegio(id);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }
    }
}