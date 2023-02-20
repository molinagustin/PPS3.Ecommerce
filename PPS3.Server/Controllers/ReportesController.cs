using Microsoft.AspNetCore.Mvc;
using PPS3.Shared.Models;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ReportesController : ControllerBase
    {
        private readonly IRepReporte _repReporte;

        public ReportesController(IRepReporte repReporte) => _repReporte = repReporte;

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Ganancia>>> ReporteGanancias([FromBody] Parametros parametros)
        {
            if (parametros == null) return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repReporte.ReporteGanancias(parametros);

                if (response != null) return Ok(response);
                else return BadRequest();
            }
            else return Problem();
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<MasProducto>>> ReporteMasProductos([FromBody] Parametros parametros)
        {
            if (parametros == null) return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repReporte.ReporteMasProductos(parametros);

                if (response != null) return Ok(response);
                else return BadRequest();
            }
            else return Problem();
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<ProductoFecha>>> ReporteProductosFecha([FromBody] Parametros parametros)
        {
            if (parametros == null) return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repReporte.ReporteProductosFecha(parametros);

                if (response != null) return Ok(response);
                else return BadRequest();
            }
            else return Problem();
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<ProductoFechaReporte>>> ReporteProductosFechaReporte([FromBody] Parametros parametros)
        {
            if (parametros == null) return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repReporte.ReporteProductosFechaReporte(parametros);

                if (response != null) return Ok(response);
                else return BadRequest();
            }
            else return Problem();
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<StockProd>>> ReporteStockProductos([FromBody] Parametros parametros)
        {
            if (parametros == null) return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repReporte.ReporteStockProductos(parametros);

                if (response != null) return Ok(response);
                else return BadRequest();
            }
            else return Problem();
        }
    }
}
