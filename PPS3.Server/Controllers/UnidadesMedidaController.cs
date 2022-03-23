using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnidadesMedidaController : ControllerBase
    {
        private readonly IRepUnidadMedida _repUnidadMedida;

        public UnidadesMedidaController(IRepUnidadMedida repUnidadMedida) => _repUnidadMedida = repUnidadMedida;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UnidadMedida>>> ObtenerUnidadesMedida()
        {
            var response = await _repUnidadMedida.ObtenerUnidadesMedida();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UnidadMedida>> ObtenerUnidadMedida(int id)
        {
            var response = await _repUnidadMedida.ObtenerUnidadMedida(id);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult> CrearUnidadMedida([FromBody] UnidadMedida unidadMedida)
        {
            if (unidadMedida == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repUnidadMedida.InsertarUnidadMedida(unidadMedida);
                if (response != false)
                    return Ok();
                else
                    return BadRequest();
            }
            else
                return Problem();
        }
        
        [HttpPut]
        public async Task<ActionResult>  ActualizarUnidadMedida([FromBody] UnidadMedida unidadMedida)
        {
            if (unidadMedida == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repUnidadMedida.ActualizarUnidadMedida(unidadMedida);
                if (response != false)
                    return Ok();
                else
                    return BadRequest();
            }
            else
                return Problem();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> BorrarUnidadMedida(int id)
        {
            var reponse = await _repUnidadMedida.BorrarUnidadMedida(id);
            if (reponse != false)
                return Ok();
            else
                return BadRequest();
        }
    }
}
