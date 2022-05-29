using PPS3.Shared.Models;

namespace PPS3.Client.Services.ServEncabezadoRecibo
{
    public class ServEncabezadoRecibo : IServEncabezadoRecibo
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorage;

        public ServEncabezadoRecibo(HttpClient httpClient, ISessionStorageService sessionStorage)
        {
            _httpClient = httpClient;
            _sessionStorage = sessionStorage;
        }

        public async Task<bool> CrearEncabRec(EncabezadoRecibo encabRec)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return false;

            //Se procede a Serializar el contenido del producto por parametro
            var encabJson = new StringContent(JsonSerializer.Serialize(encabRec), Encoding.UTF8, "application/json");

            //Creo el objeto donde se guardara el mensaje devuelto
            var response = new HttpResponseMessage();            

            //Creo una solicitud Http de tipo POST
            var request = new HttpRequestMessage(HttpMethod.Post, $"api/EncabezadosRecibos/CrearNuevoRecibo");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);
            //Agrego el JSON al BODY
            request.Content = encabJson;
            //Envio la solicitud HTTP
            response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<EncabezadoRecibo> ObtenerEncab(int idRecibo)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/EncabezadosRecibos/ObtenerRecibo/{idRecibo}");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bits) y lo deserializo en un objeto apropiado
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var encab = await JsonSerializer.DeserializeAsync<EncabezadoRecibo>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return encab;
            }
            else
                return null;
        }

        public async Task<IEnumerable<EncabezadoRecibo>> ObtenerEncabRecCliente(int idCliente)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/EncabezadosRecibos/ObtenerRecibosCliente/{idCliente}");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bits) y lo deserializo en un objeto apropiado
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var encabs = await JsonSerializer.DeserializeAsync<IEnumerable<EncabezadoRecibo>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return encabs;
            }
            else
                return null;
        }
    }
}
