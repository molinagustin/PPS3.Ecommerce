using PPS3.Shared.Models;

namespace PPS3.Client.Services.ServCuerpoComprobante
{
    public class ServCuerpoComprobante : IServCuerpoComprobante
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorage;

        public ServCuerpoComprobante(HttpClient httpClient, ISessionStorageService sessionStorage)
        {
            _httpClient = httpClient;
            _sessionStorage = sessionStorage;
        }

        public async Task<bool> CrearCuerpo(CuerpoComprobante cuerpoComp)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return false;

            //Se procede a Serializar el contenido del producto por parametro
            var cuerpoJson = new StringContent(JsonSerializer.Serialize(cuerpoComp), Encoding.UTF8, "application/json");

            //Creo el objeto donde se guardara el mensaje devuelto
            var response = new HttpResponseMessage();
                        
            //Creo una solicitud Http de tipo POST
            var request = new HttpRequestMessage(HttpMethod.Post, $"api/CuerposComprobantes/CrearCuerpoComp");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);
            //Agrego el JSON al BODY
            request.Content = cuerpoJson;
            //Envio la solicitud HTTP
            response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            
            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<CuerpoComprobante> ObtenerCuerpo(int numCuerpo)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/CuerposComprobantes/ObtenerCuerpoComp?numCuerpo={numCuerpo}");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bits) y lo deserializo en un objeto apropiado
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var cuerpo = await JsonSerializer.DeserializeAsync<CuerpoComprobante>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return cuerpo;
            }
            else
                return null;
        }

        public async Task<IEnumerable<CuerpoComprobante>> ObtenerCuerpos(int numCabeceraComp)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/CuerposComprobantes/ObtenerCuerposDeComprobante?numCabeceraComp={numCabeceraComp}");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bits) y lo deserializo en un objeto apropiado
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var cuerpos = await JsonSerializer.DeserializeAsync<IEnumerable<CuerpoComprobante>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return cuerpos;
            }
            else
                return null;
        }
    }
}
