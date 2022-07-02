using Microsoft.AspNetCore.Mvc;
using PPS3.Shared.Models;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EncabezadosRecibosController : ControllerBase
    {
        private readonly IRepEncabezadoRecibo _repEncabRec;

        public EncabezadosRecibosController(IRepEncabezadoRecibo repEncabRec) => _repEncabRec = repEncabRec;
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<EncabezadoRecibo>>> ObtenerRecibosCliente(int idCliente)
        {
            if (idCliente <= 0)
                return BadRequest();

            var response = await _repEncabRec.ObtenerRecibosPorCliente(idCliente);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<EncabezadoRecibo>> ObtenerRecibo(int idRecibo)
        {
            if (idRecibo <= 0)
                return BadRequest();

            var response = await _repEncabRec.ObtenerRecibo(idRecibo);
            return Ok(response);
        }

        [HttpGet("{idCliente:int}")]
        public async Task<ActionResult<IEnumerable<Recibo>>> ObtenerRecibosListPorCliente(int idCliente)
        {
            if (idCliente <= 0)
                return BadRequest();

            var response = await _repEncabRec.ObtenerRecibosListPorCliente(idCliente);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Recibo>>> ObtenerRecibosList()
        {
            var response = await _repEncabRec.ObtenerRecibosList();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CrearNuevoRecibo([FromBody] EncabezadoRecibo encabRec)
        {
            if (encabRec == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repEncabRec.InsertarRecibo(encabRec);
                if (response > 0)
                    return Ok(response);
                else
                    return BadRequest();
            }
            else
                return Problem();
        }
    }
}
