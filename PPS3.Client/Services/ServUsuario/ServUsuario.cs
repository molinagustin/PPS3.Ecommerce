using System.Text.Json;
using System.Text;
using PPS3.Shared.Models;

namespace PPS3.Client.Services.ServUsuario
{
    public class ServUsuario : IServUsuario
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorage;

        //Se inyecta el servicio HTTP y SESSION STORAGE
        public ServUsuario(HttpClient httpClient, ISessionStorageService sessionStorage)
        {
            _httpClient = httpClient;
            _sessionStorage = sessionStorage;
        }

        public async Task<bool> ActualizarUsuario(Usuario usuario)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return false;

            //Se procede a Serializar el contenido del objeto por parametro
            var usuarioJson = new StringContent(JsonSerializer.Serialize(usuario), Encoding.UTF8, "application/json");

            //Creo el objeto donde se guardara el mensaje devuelto
            var response = new HttpResponseMessage();

            //Creo una solicitud Http de tipo PUT
            var request = new HttpRequestMessage(HttpMethod.Put, $"api/Usuarios/ActualizarUsuario");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);
            //Agrego el JSON al BODY
            request.Content = usuarioJson;
            //Envio la solicitud HTTP
            response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<bool> BorrarUsuario(int id)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return false;

            //Creo una solicitud Http de tipo delete
            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/Usuarios/BorrarUsuario/{id}");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Verifico que la respuesta sea exitosa
            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<bool> Login(UsuarioRequest usuarioReq)
        {
            //Serializo el objeto
            var usuarioJson = new StringContent(JsonSerializer.Serialize(usuarioReq),
                Encoding.UTF8, "application/json");

            //Guardo el token devuelto en caso exitoso
            var token = await _httpClient.PostAsync("api/Usuarios/Login", usuarioJson);

            //Si hubo una respuesta exitosa, guardo en el SESSION STORAGE los datos
            if (token.IsSuccessStatusCode)
            {
                await _sessionStorage.SetItemAsync("token", await token.Content.ReadAsStringAsync());
                return true;
            }
            else            
                return false;            
        }

        public async Task<bool> Logout()
        {
            await _sessionStorage.RemoveItemAsync("token");
            return true;
        }

        public async Task<Usuario> ObtenerUsuario(int id)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Usuarios/ObtenerUsuario/{id}");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bites) y lo deserializo en un objeto apropiado
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var usuario = await JsonSerializer.DeserializeAsync<Usuario>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return usuario;
            }
            else
                return null;
        }

        public async Task<Usuario> ObtenerUsuario(string nombreUsuario)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Usuarios/ObtenerUsuario/{nombreUsuario}");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bites) y lo deserializo en un objeto apropiado
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var usuario = await JsonSerializer.DeserializeAsync<Usuario>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return usuario;
            }
            else
                return null;
        }

        public async Task<IEnumerable<Usuario>> ObtenerUsuarios()
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/Usuarios/ObtenerUsuarios");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bites) y lo deserializo en un objeto apropiado (Enumerable de Usuarios)
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var usuarios = await JsonSerializer.DeserializeAsync<IEnumerable<Usuario>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return usuarios;
            }
            else
                return null;
        }

        public async Task<bool> Registrarse(UsuarioCliente usuarioCl)
        {
            //Serializo el objeto
            var usuarioJson = new StringContent(JsonSerializer.Serialize(usuarioCl),
                Encoding.UTF8, "application/json");

            //Guardo el resultado
            var response = await _httpClient.PostAsync("api/Usuarios/CrearUsuario", usuarioJson);

            //Verifico la respuesta exitosa
            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }
    }
}