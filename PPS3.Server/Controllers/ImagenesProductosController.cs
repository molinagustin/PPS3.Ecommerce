using Microsoft.AspNetCore.Mvc;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ImagenesProductosController : ControllerBase
    {
        private readonly IRepImagenProducto _repImagenProducto;

        public ImagenesProductosController(IRepImagenProducto repImagenProducto) => _repImagenProducto = repImagenProducto;

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImagenProducto>>> ObtenerImagenes(int idProducto)
        {
            var response = await _repImagenProducto.ObtenerImagenes(idProducto);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImagenProducto>>> ObtenerUltimasImagenes()
        {
            var response = await _repImagenProducto.ObtenerUltimasImagenes();
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }

        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ImagenProducto>> ObtenerImagen(int id)
        {
            var response = await _repImagenProducto.ObtenerImagen(id);
            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult> CrearImagenProducto([FromBody] ImagenProducto imagenProducto)
        {
            if (imagenProducto == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repImagenProducto.InsertarImagen(imagenProducto);
                if (response != false)
                    return Ok();
                else
                    return BadRequest();
            }
            else
                return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> ActualizarImagenProducto([FromBody] ImagenProducto imagenProducto)
        {
            if (imagenProducto == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repImagenProducto.ActualizarImagen(imagenProducto);
                if (response != false)
                    return Ok();
                else
                    return BadRequest();
            }
            else
                return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> ImagenFavorita([FromBody] ImagenProducto imagenFav)
        {
            if (imagenFav == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repImagenProducto.ImagenFavorita(imagenFav);
                if (response != false)
                    return Ok();
                else
                    return BadRequest();
            }
            else
                return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> BorrarImagenProducto(int id)
        {
            var response = await _repImagenProducto.BorrarImagen(id);
            if (response != false)
                return Ok();
            else
                return BadRequest();
        }
    }
}
