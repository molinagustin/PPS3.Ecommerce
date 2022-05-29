namespace PPS3.Client.Services.ServTipoVenta
{
    public class ServTipoVenta : IServTipoVenta
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorage;

        public ServTipoVenta(HttpClient httpClient, ISessionStorageService sessionStorage)
        {
            _httpClient = httpClient;
            _sessionStorage = sessionStorage;
        }
        
        public async Task<IEnumerable<TipoVenta>> ObtenerTiposVentas()
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/TiposVentas/ObtenerTiposVentas");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bits) y lo deserializo en un objeto apropiado
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var ventas = await JsonSerializer.DeserializeAsync<IEnumerable<TipoVenta>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return ventas;
            }
            else
                return null;

            //var response = await _httpClient.GetStreamAsync("api/TiposVentas");

            //var tiposVta = await JsonSerializer.DeserializeAsync<IEnumerable<TipoVenta>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            //return tiposVta;
        }

        public async Task<TipoVenta> ObtenerTipoVenta(int id)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return null;

            //Creo una solicitud Http de tipo GET
            var request = new HttpRequestMessage(HttpMethod.Get, $"api/TiposVentas/ObtenerTipoVenta/{id}");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bits) y lo deserializo en un objeto apropiado
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var venta = await JsonSerializer.DeserializeAsync<TipoVenta>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return venta;
            }
            else
                return null;

            //var response = await _httpClient.GetStreamAsync($"api/TiposVentas/{id}");

            //var tipoVta = await JsonSerializer.DeserializeAsync<TipoVenta>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            //return tipoVta;
        }
    }
}