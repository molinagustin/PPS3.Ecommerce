using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PPS3.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IRepUsuario _repUsuario;
        private readonly IRepCliente _repCliente;
        private readonly IRepPrivilegio _repPrivilegio;
        private readonly IConfiguration _config;

        public UsuariosController(IRepUsuario repUsuario, IRepCliente repCliente, IRepPrivilegio repPrivilegio, IConfiguration config)
        {
            _repUsuario = repUsuario;
            _repCliente = repCliente;
            _repPrivilegio = repPrivilegio;
            _config = config;
        } 

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> ObtenerUsuarios()
        {
            var response = await _repUsuario.ObtenerUsuarios();
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Usuario>> ObtenerUsuario(int id)
        {
            var response = await _repUsuario.ObtenerUsuario(id);
            if (response != null)
                return Ok(response);
            else
                return NotFound();            
        }

        [HttpGet("{nombreUsuario}")]
        public async Task<ActionResult<Usuario>> ObtenerUsuario(string nombreUsuario)
        {
            var response = await _repUsuario.ObtenerUsuario(nombreUsuario);
            if (response != null)
                return Ok(response);
            else
                return NotFound();
        }

        //El Registro de un nuevo usuario.
        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> CrearUsuario([FromBody] UsuarioCliente usuarioCliente)
        {
            if (usuarioCliente == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                //Verifico que no existe otro usuario con el mismo nombre de usuario
                var existe = await _repUsuario.UsuarioExistente(usuarioCliente.NombreUs);
                if (existe)
                    return BadRequest(new { message = "El nombre de usuario que intenta utlizar no esta disponible, por favor introduzca otro." });

                //Luego de la verificacion del modelo valido y que el nombre de usuario este disponible, creo un nuevo cliente y traigo su id, el cual, si es mayor a 0 se lo asigno al usuario que estoy creando
                var clienteResponse = await _repCliente.CrearCliente(usuarioCliente);
                if (clienteResponse > 0)
                {
                    usuarioCliente.IdCliente = clienteResponse;
                    var response = await _repUsuario.CrearUsuario(usuarioCliente);
                    if (response != false)
                        return Ok();
                    else
                        return BadRequest();
                }
                else
                    return BadRequest();                
            }
            else
                return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> ActualizarUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repUsuario.ActualizarUsuario(usuario);
                if (response != false)
                    return Ok();
                else
                    return BadRequest();
            }
            else
                return Problem();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> BorrarUsuario(int id)
        {
            var response = await _repUsuario.BorrarUsuario(id);
            if (response != false)
                return Ok();
            else
                return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<string>> Login([FromBody] UsuarioRequest usuarioRequest)
        {
            if (usuarioRequest == null)
                return Problem();

            if (!ModelState.IsValid)
                return Problem();

            //Verifico si existe el usuario en la base de datos
            Usuario usuarioOriginal = await _repUsuario.ObtenerUsuario(usuarioRequest.NombreUs);
            if (usuarioOriginal == null)
                return BadRequest(new { message = "Usuario o Contraseña Incorrecto." });

            //Valido que la contraseña ingresada coincida y genere el mismo hash que posee el usuario original
            if (!_repUsuario.ValidarHash(usuarioRequest.Password, usuarioOriginal.SaltCont, usuarioOriginal.HashCont))
                return BadRequest(new { message = "Usuario o Contraseña Incorrecto." });

            //Verifico que el usuario este Activo
            if (usuarioOriginal.Activo == false)
                return BadRequest(new { message = "El Usuario se encuentra dado de Baja." });

            //Ahora comienzo a crear el token para el usuario registrado
            var privilegio = await _repPrivilegio.ObtenerPrivilegio(usuarioOriginal.Privilegio);

            //Este objeto se encarga de crear nuevos JWT para lo cual vuelvo a usar la clave secreta de configuracion
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.UTF8.GetBytes(_config["ClaveSimetrica:Key"]); //Esa es la forma para acceder a appsettings.json

            //El token descriptor ayuda a configurar los datos propios del usuario, tiempo de vida del token, formas de cifrado, entre otros
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                //Datos del usuario
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, usuarioOriginal.NombreUs),
                    new Claim(ClaimTypes.Role, privilegio.DescPrivilegio)
                }),

                //Fecha de expiracion del token
                Expires = DateTime.UtcNow.AddDays(1),
                //Se crean las credenciales con una clave simetrica y el algoritmo de encriptado
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            //Por ultimo se crea el token de seguridad con los datos configurados en el Descriptor, el cual se asigna al usuario correspondiente
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            usuarioOriginal.Token = tokenHandler.WriteToken(token);

            //Se comprueba que se haya escrito exitosamente y en caso afirmatio se lo devuelve al token
            if (usuarioOriginal.Token == null || usuarioOriginal.Token == string.Empty)
                return BadRequest(new { message = "Usuario o Contraseña Incorrecto" });

            return Ok(usuarioOriginal.Token);
        }
    }
}