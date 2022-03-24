using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalidadesController : ControllerBase
    {
        private readonly IRepLocalidad _repLocalidad;

        public LocalidadesController(IRepLocalidad repLocalidad) => _repLocalidad = repLocalidad;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Localidad>>> ObtenerLocalidades()
        {
            var response = await _repLocalidad.ObtenerLocalidades();
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Localidad>> ObtenerLocalidad(int id)
        {
            var response = await _repLocalidad.ObtenerLocalidad(id);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }


        [HttpGet("{nombreLocalidad}")]
        public async Task<ActionResult<Localidad>> ObtenerLocalidad(string nombreLocalidad)
        {
            var response = await _repLocalidad.ObtenerLocalidad(nombreLocalidad);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult> CrearLocalidad([FromBody] Localidad localidad)
        {
            if (localidad == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repLocalidad.InsertarLocalidad(localidad);
                if (response != false)
                    return Ok();
                else
                    return BadRequest();
            }
            else
                return Problem();
        }

        [HttpPut]
        public async Task<ActionResult> ActualizarLocalidad([FromBody] Localidad localidad)
        {
            if (localidad == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repLocalidad.ActualizarLocalidad(localidad);
                if (response != false)
                    return Ok();
                else
                    return BadRequest();
            }
            else
                return Problem();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> BorrarLocalidad(int id)
        {
            var response = await _repLocalidad.BorrarLocalidad(id);
            if (response != false)
                return Ok();
            else
                return BadRequest();
        }
    }
}