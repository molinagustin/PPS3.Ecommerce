namespace PPS3.Client.Services.ServFormaPago
{
    public class ServFormaPago : IServFormaPago
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorage;

        public ServFormaPago(HttpClient httpClient, ISessionStorageService sessionStorage)
        {
            _httpClient = httpClient;
            _sessionStorage = sessionStorage;
        }

        public async Task<FormaPago> ObtenerFormaPago(int id)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/FormasPago/{id}");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bits) y lo deserializo en un objeto apropiado
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var forma = await JsonSerializer.DeserializeAsync<FormaPago>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return forma;
            }
            else
                return null;
        }

        public async Task<IEnumerable<FormaPago>> ObtenerFormasPago()
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/FormasPago");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bits) y lo deserializo en un objeto apropiado
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var formas = await JsonSerializer.DeserializeAsync<IEnumerable<FormaPago>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return formas;
            }
            else
                return null;
        }
    }
}
