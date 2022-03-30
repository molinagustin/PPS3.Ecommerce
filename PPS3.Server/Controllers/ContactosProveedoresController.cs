using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactosProveedoresController : ControllerBase
    {
        private readonly IRepContactoProveedor _repContactoProveedor;

        public ContactosProveedoresController(IRepContactoProveedor repContactoProveedor) => _repContactoProveedor = repContactoProveedor;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactoProveedor>>> ObtenerTodosContactos()
        {
            var response = await _repContactoProveedor.ObtenerTodosContactos();
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ContactoProveedor>> ObtenerContactoProveedor(int id)
        {
            var response = await _repContactoProveedor.ObtenerContactoProveedor(id);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }

        [HttpGet("{nombreContacto}")]
        public async Task<ActionResult<ContactoProveedor>> ObtenerContactoProveedor(string nombreContacto)
        {
            var response = await _repContactoProveedor.ObtenerContactoProveedor(nombreContacto);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult> CrearNuevoContacto([FromBody] ContactoProveedor contactoProveedor)
        {
            if (contactoProveedor == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repContactoProveedor.InsertarContactoProveedor(contactoProveedor);
                if (response != false)
                    return Ok();
                else
                    return BadRequest();
            }
            else
                return Problem();
        }

        [HttpPut]
        public async Task<ActionResult> ActualizarContactoProveedor([FromBody] ContactoProveedor contactoProveedor)
        {
            if (contactoProveedor == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repContactoProveedor.ActualizarContactoProveedor(contactoProveedor);
                if (response != false)
                    return Ok();
                else
                    return BadRequest();
            }
            else
                return Problem();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> BorrarContactoProveedor(int id)
        {
            var response = await _repContactoProveedor.BorrarContactoProveedor(id);
            if (response != false)
                return Ok();
            else
                return BadRequest();
        }
    }
}
