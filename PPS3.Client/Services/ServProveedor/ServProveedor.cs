using PPS3.Shared.Models;

namespace PPS3.Client.Services.ServProveedor
{
    public class ServProveedor : IServProveedor
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorage;

        public ServProveedor(HttpClient httpClient, ISessionStorageService sessionStorage)
        {
            _httpClient = httpClient;
            _sessionStorage = sessionStorage;
        }
        
        public async Task<bool> BorrarProveedor(int id)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return false;

            //Creo una solicitud Http de tipo delete
            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/Proveedores/{id}");
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

        public async Task<bool> GuardarProveedor(Proveedor proveedor)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return false;

            //Se procede a Serializar el contenido del producto por parametro
            var provJson = new StringContent(JsonSerializer.Serialize(proveedor), Encoding.UTF8, "application/json");

            //Creo el objeto donde se guardara el mensaje devuelto
            var response = new HttpResponseMessage();

            //Si posee un ID es una actualizacion (PUT)
            if (proveedor.IdProveedor > 0)
            {
                //Creo una solicitud Http de tipo PUT
                var request = new HttpRequestMessage(HttpMethod.Put, $"api/Proveedores");
                //Agrego el token al Encabezado Http
                request.Headers.Add("Authorization", "Bearer " + token);
                //Agrego el JSON al BODY
                request.Content = provJson;
                //Envio la solicitud HTTP
                response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            }
            else
            {
                //Creo una solicitud Http de tipo POST
                var request = new HttpRequestMessage(HttpMethod.Post, $"api/Proveedores");
                //Agrego el token al Encabezado Http
                request.Headers.Add("Authorization", "Bearer " + token);
                //Agrego el JSON al BODY
                request.Content = provJson;
                //Envio la solicitud HTTP
                response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            }

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<Proveedor> ObtenerProveedor(int id)
        {
            var response = await _httpClient.GetStreamAsync($"api/Proveedores/{id}");

            var proveedor = await JsonSerializer.DeserializeAsync<Proveedor>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return proveedor;
        }

        public async Task<IEnumerable<Proveedor>> ObtenerProveedores()
        {
            var response = await _httpClient.GetStreamAsync($"api/Proveedores");

            var proveedores = await JsonSerializer.DeserializeAsync<IEnumerable<Proveedor>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return proveedores;
        }
    }
}
