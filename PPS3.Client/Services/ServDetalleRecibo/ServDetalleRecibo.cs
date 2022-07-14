using PPS3.Shared.InternalModels;
using PPS3.Shared.Models;

namespace PPS3.Client.Services.ServDetalleRecibo
{
    public class ServDetalleRecibo : IServDetalleRecibo
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorage;

        public ServDetalleRecibo(HttpClient httpClient, ISessionStorageService sessionStorage)
        {
            _httpClient = httpClient;
            _sessionStorage = sessionStorage;
        }               

        public async Task<bool> CrearDetalleRecibo(DetalleRecibo detalleRec)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return false;

            //Se procede a Serializar el contenido del producto por parametro
            var detalleJson = new StringContent(JsonSerializer.Serialize(detalleRec), Encoding.UTF8, "application/json");

            //Creo el objeto donde se guardara el mensaje devuelto
            var response = new HttpResponseMessage();
            
            //Creo una solicitud Http de tipo POST
            var request = new HttpRequestMessage(HttpMethod.Post, $"api/DetallesRecibos/CrearDetalleRecibo");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);
            //Agrego el JSON al BODY
            request.Content = detalleJson;
            //Envio la solicitud HTTP
            response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            
            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<IEnumerable<DetalleRecibo>> ObtenerDetallesPorComprobante(int idComprobante)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/DetallesRecibos/ObtenerDetallesPorComprobante?idComprobante={idComprobante}");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bits) y lo deserializo en un objeto apropiado
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var detalles = await JsonSerializer.DeserializeAsync<IEnumerable<DetalleRecibo>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return detalles;
            }
            else
                return null;
        }

        public async Task<IEnumerable<DetalleRecibo>> ObtenerDetallesRecibo(int idRecibo)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/DetallesRecibos/ObtenerDetallesRecibo?idRecibo={idRecibo}");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bits) y lo deserializo en un objeto apropiado
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var detalles = await JsonSerializer.DeserializeAsync<IEnumerable<DetalleRecibo>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return detalles;
            }
            else
                return null;
        }

        public async Task<IEnumerable<DetalleRecibo>> ObtenerDetallesRecibos()
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/DetallesRecibos/ObtenerDetallesRecibos");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bits) y lo deserializo en un objeto apropiado
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var detalles = await JsonSerializer.DeserializeAsync<IEnumerable<DetalleRecibo>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return detalles;
            }
            else
                return null;
        }
    }
}
