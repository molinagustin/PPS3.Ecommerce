using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using PPS3.Shared.InternalModels;
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
        private readonly IRepEncabezadoCuentaCorriente _repCuentaCorriente;
        private readonly IRepPrivilegio _repPrivilegio;
        private readonly IServEmail _servEmail;
        private readonly IConfiguration _config;

        public UsuariosController(IRepUsuario repUsuario, IRepCliente repCliente, IRepEncabezadoCuentaCorriente repCuentaCorriente, IRepPrivilegio repPrivilegio, IServEmail servEmail, IConfiguration config)
        {
            _repUsuario = repUsuario;
            _repCliente = repCliente;
            _repCuentaCorriente = repCuentaCorriente;
            _repPrivilegio = repPrivilegio;
            _servEmail = servEmail;
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
                    //Si se creo el usuario, procedo a crearle la cuenta corriente
                    if (response > 0)
                    {
                        //Le creo su cuenta corriente, asignando el Id del Cliente y el Id del Usuario
                        EncabezadoCuentaCorriente nuevaCuenta = new EncabezadoCuentaCorriente();
                        nuevaCuenta.ClienteCC = clienteResponse;
                        nuevaCuenta.UsuarioCrea = response;
                        nuevaCuenta.UsuarioModif = response;

                        var nuevaCuentaCreada = await _repCuentaCorriente.InsertarCuentaCorriente(nuevaCuenta);

                        //Si fue creada correctamente, devuelvo OK
                        if(nuevaCuentaCreada)
                        {
                            //Envio un email para su verificacion
                            EmailAutenticacion datosEmail = new EmailAutenticacion();
                            datosEmail.Destinatario = usuarioCliente.Email;
                            datosEmail.Usuario = response;
                            _servEmail.EmailVerificacion(datosEmail);
                            return Ok();
                        }                            
                        else
                            return BadRequest();
                    }                       
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
                    return StatusCode(500);
            }
            else
                return Problem();
        }

        [HttpPut]
        public async Task<ActionResult> ActualizarPerfilUsuario([FromBody] UsuarioCliente usuario)
        {
            if (usuario == null)
                return BadRequest();            

            if (ModelState.IsValid)
            {
                //Valido que la contraseña ingresada coincida y genere el mismo hash que posee el usuario original
                if (!_repUsuario.ValidarHash(usuario.Password, usuario.SaltCont, usuario.HashCont))
                    return BadRequest();

                var response = await _repUsuario.ActualizarPerfilUsuario(usuario);
                if (response != false)
                    return Ok();
                else
                    return StatusCode(500);
            }
            else
                return Problem();
        }

        [HttpPut]
        public async Task<ActionResult> CambiarPassword([FromBody] UsuarioCliente usuario)
        {
            if (usuario == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                var response = await _repUsuario.CambiarPassword(usuario);
                if (response != false)
                    return Ok();
                else
                    return StatusCode(500);
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

            //Lista de Claims
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.PrimarySid, usuarioOriginal.IdUsuarioAct.ToString()),
                new Claim(ClaimTypes.Name, usuarioOriginal.NombreUs),
                new Claim(ClaimTypes.Role, privilegio.IdPrivi.ToString()),
                new Claim(ClaimTypes.Actor, usuarioOriginal.IdCliente.ToString())
            };

            //Clave simetrica para generar el token obtenida desde el archivo de configuracion
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("ClaveSimetrica:Key").Value));

            //Se crean las credenciales con una clave simetrica y el algoritmo de encriptado
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            //Se asignan los parametros que tendra el token en base a los objetos creados anteriormente
            var token = new JwtSecurityToken
                (
                    claims: claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: credentials
                );

            //Se escribe el Token finalmente
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            //Se comprueba que se haya escrito exitosamente y en caso afirmativo se lo devuelve al token
            if (String.IsNullOrEmpty(jwt))
                return BadRequest(new { message = "Usuario o Contraseña Incorrecto" });

            return Ok(jwt);            
        }
    }
}