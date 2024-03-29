﻿using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RubrosController : ControllerBase
    {
        private readonly IRepRubro _repRubro;

        public RubrosController(IRepRubro repRubro) => _repRubro = repRubro;

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rubro>>> ObtenerRubros()
        {
            var response = await _repRubro.ObtenerRubros();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RubroCategoria>>> ObtenerRubrosCategorias()
        {
            var response = await _repRubro.ObtenerRubrosCategorias();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoProductoCategoria>>> ObtenerTiposProductosCategorias()
        {
            var response = await _repRubro.ObtenerTiposProductosCategorias();
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult<Rubro>> ObtenerRubro(int id)
        {
            var response = await _repRubro.ObtenerRubro(id);
            
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RubroListado>>> ObtenerRubrosListado()
        {
            var response = await _repRubro.ObtenerRubrosListado();
            return Ok(response);
        }

        [HttpGet("{idRubro}")]
        public async Task<ActionResult<int>> CantidadTiposProductosActivos(int idRubro)
        {
            var response = await _repRubro.CantidadTiposProductosActivos(idRubro);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CrearRubro([FromBody] Rubro rubro)
        { 
            if (rubro == null)  
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repRubro.InsertarRubro(rubro);
                if (response > 0)
                    return Ok(response);
                else
                    return BadRequest();
            }
            else
                return Problem();
        }

        [HttpPut]
        public async Task<ActionResult<int>> ActualizarRubro([FromBody] Rubro rubro)
        {
            if (rubro == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repRubro.ActualizarRubro(rubro);
                if (response > 0)
                    return Ok(response);
                else
                    return BadRequest();
            }
            else
                return Problem();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> BorrarRubro(int id)
        {
            var response = await _repRubro.BorrarRubro(id);
            if (response != false)
                return Ok();
            else
                return Problem();
        }
    }
}
