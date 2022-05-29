namespace PPS3.Client.Services.ServTipoComprobante
{
    public class ServTipoComprobante : IServTipoComprobante
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorage;

        public ServTipoComprobante(HttpClient httpClient, ISessionStorageService sessionStorage)
        {
            _httpClient = httpClient;
            _sessionStorage = sessionStorage;
        }
        
        public async Task<TipoComprobante> ObtenerTipoComp(int id)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/TiposComprobantes/{id}");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bites) y lo deserializo en un objeto apropiado
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var comp = await JsonSerializer.DeserializeAsync<TipoComprobante>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return comp;
            }
            else
                return null;
        }

        public async Task<IEnumerable<TipoComprobante>> ObtenerTiposComp()
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/TiposComprobantes");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bites) y lo deserializo en un objeto apropiado
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var comps = await JsonSerializer.DeserializeAsync<IEnumerable<TipoComprobante>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return comps;
            }
            else
                return null;
        }
    }
}
