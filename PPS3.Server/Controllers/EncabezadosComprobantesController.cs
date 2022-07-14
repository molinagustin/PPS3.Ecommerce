using Microsoft.AspNetCore.Mvc;
using PPS3.Shared.Models;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EncabezadosComprobantesController : ControllerBase
    {
        private readonly IRepEncabezadoComprobante _repEncabComp;

        public EncabezadosComprobantesController(IRepEncabezadoComprobante repEncabComp) => _repEncabComp = repEncabComp;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EncabezadoComprobante>>> ObtenerEncabezados()
        {
            var response = await _repEncabComp.ObtenerEncabezadosComp();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EncabezadoComprobante>> ObtenerEncabezado(int id)
        {
            var response = await _repEncabComp.ObtenerEncabezadoComp(id);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }

        [HttpGet("{idCliente}")]
        public async Task<ActionResult<IEnumerable<Comprobante>>> ObtenerComprobantesListCliente(int idCliente)
        {
            var response = await _repEncabComp.ObtenerComprobantesListCliente(idCliente);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comprobante>>> ObtenerComprobantesList()
        {
            var response = await _repEncabComp.ObtenerComprobantesList();
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DetalleComprobante>>> ObtenerDetallesComprobantesList()
        {
            var response = await _repEncabComp.ObtenerDetallesComprobantesList();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CrearEncabezadoComp([FromBody] EncabezadoComprobante encabezadoComp)
        {
            if (encabezadoComp == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repEncabComp.InsertarEncabezadoComp(encabezadoComp);
                //Devuelvo el ID del comprobante ingresado
                if (response > 0)
                    return Ok(response);
                else
                    return BadRequest();
            }
            else
                return Problem();
        }

        [HttpPut]
        public async Task<ActionResult> ActualizarComprobante([FromBody]Comprobante comprobante)
{
            if (comprobante == null)
                return BadRequest();

            if (ModelState.IsValid)
{
                var response = await _repEncabComp.ActualizarComprobante(comprobante);
                if (response != false)
                    return Ok();
                else
                    return BadRequest();
            }
            else
                return Problem();
        }
    }
}
