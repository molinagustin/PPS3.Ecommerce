using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    //El Decorador [Authorize] indica que el usuario debera estar Autorizado o Iniciado Sesion con un token para poder acceder a los distintos metodos.
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        //Se crea un campo donde se inyectara el servicio de IRepProducto en el constructor
        private readonly IRepProducto _repProducto;

        public ProductosController(IRepProducto repProducto) => _repProducto = repProducto;

        //En el metodo Get se va a devolver un ActionResult que contiene la respuesta al servicio solicitado, en este caso se devuelve Ok en conjunto con el listado de productos de la base de datos
        //El Decorador [AllowAnonymous] permite que a pesar de que haya que estar Autorizado para acceder a las llamadas HTTP, en estos casos, se pueda hacer de forma Anonima, es decir, sin estar Autorizado o Iniciado Sesion
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Producto>>> ObtenerProductos()
        {
            var response = await _repProducto.ObtenerProductos();
            return Ok(response);
        }

        //Similar al metodo anterior, solo que en la solicitud HTTP va a ir un parametro, que sera el ID del producto que solicitamos
        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<Producto>> ObtenerProducto(int id)
        {
            var response = await _repProducto.ObtenerProducto(id);

            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<Producto>> ObtenerProducto(string nombreProd)
        {
            var response = await _repProducto.ObtenerProducto(nombreProd);

            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult> CrearProducto([FromBody] Producto producto)
        {
            if (producto == null)
                return BadRequest();

            //El model state se asegura de que si hay alguna propiedad o campo requerido o con condiciones en el modelo, deban ser respetadas, sino lanzara el error.
            if (ModelState.IsValid)
            {
                var response = await _repProducto.InsertarProducto(producto);

                if (response != false)
                    return Ok();
                else
                    return BadRequest();
            }
            else
                return Problem();            
        }

        [HttpPut]
        public async Task<ActionResult> ActualizarProducto([FromBody] Producto producto)
        {
            if (producto == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repProducto.ActualizarProducto(producto);
                if (response != false)
                    return Ok();
                else
                    //El codigo 500 indica que hubo algun problema con el servidor o la conexion a la base de datos
                    return StatusCode(500);
            }
            else
                return Problem();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> BorrarProducto(int id)
        {
            var response = await _repProducto.BorrarProducto(id);
            if (response != false)
                return Ok();
            else
                return BadRequest();
        }
    }
}
