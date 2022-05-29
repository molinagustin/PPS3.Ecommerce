using PPS3.Shared.Models;

namespace PPS3.Client.Services.ServContactoProveedor
{
    public class ServContactoProveedor : IServContactoProveedor
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorage;

        public ServContactoProveedor(HttpClient httpClient, ISessionStorageService sessionStorage)
        {
            _httpClient = httpClient;
            _sessionStorage = sessionStorage;
        }

        public async Task<bool> BorrarContacto(int id)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return false;

            //Creo una solicitud Http de tipo delete
            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/ContactosProveedores/{id}");
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

        public async Task<bool> GuardarContacto(ContactoProveedor contacto)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return false;

            //Se procede a Serializar el contenido del producto por parametro
            var contactoJson = new StringContent(JsonSerializer.Serialize(contacto), Encoding.UTF8, "applicattion/json");

            //Creo el objeto donde se guardara el mensaje devuelto
            var response = new HttpResponseMessage();

            //Si posee un ID es una actualizacion (PUT)
            if (contacto.IdProvCont > 0)
            {
                //Creo una solicitud Http de tipo PUT
                var request = new HttpRequestMessage(HttpMethod.Put, $"api/ContactosProveedores");
                //Agrego el token al Encabezado Http
                request.Headers.Add("Authorization", "Bearer " + token);
                //Agrego el JSON al BODY
                request.Content = contactoJson;
                //Envio la solicitud HTTP
                response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            }
            else
            {
                //Creo una solicitud Http de tipo POST
                var request = new HttpRequestMessage(HttpMethod.Post, $"api/ContactosProveedores");
                //Agrego el token al Encabezado Http
                request.Headers.Add("Authorization", "Bearer " + token);
                //Agrego el JSON al BODY
                request.Content = contactoJson;
                //Envio la solicitud HTTP
                response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            }

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<ContactoProveedor> ObtenerConctacto(string nombreContacto)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/ContactosProveedores/{nombreContacto}");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bits) y lo deserializo en un objeto apropiado
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var contacto = await JsonSerializer.DeserializeAsync<ContactoProveedor>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return contacto;
            }
            else
                return null;
        }

        public async Task<ContactoProveedor> ObtenerContacto(int id)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/ContactosProveedores/{id}");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bits) y lo deserializo en un objeto apropiado
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var contacto = await JsonSerializer.DeserializeAsync<ContactoProveedor>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return contacto;
            }
            else
                return null;
        }

        public async Task<IEnumerable<ContactoProveedor>> ObtenerContactos()
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/ContactosProveedores");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bits) y lo deserializo en un objeto apropiado
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var contactos = await JsonSerializer.DeserializeAsync<IEnumerable<ContactoProveedor>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return contactos;
            }
            else
                return null;
        }
    }
}