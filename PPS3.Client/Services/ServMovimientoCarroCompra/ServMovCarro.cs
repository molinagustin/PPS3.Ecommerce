namespace PPS3.Client.Services.ServMovimientoCarroCompra
{
    public class ServMovCarro : IServMovCarro
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorage;

        public ServMovCarro(HttpClient httpClient, ISessionStorageService sessionStorage)
        {
            _httpClient = httpClient;
            _sessionStorage = sessionStorage;
        }

        public async Task<bool> CrearMovimiento(MovimientoCarroCompra movimiento)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return false;

            //Se procede a Serializar el contenido del producto por parametro
            var movJson = new StringContent(JsonSerializer.Serialize(movimiento), Encoding.UTF8, "application/json");

            //Creo el objeto donde se guardara el mensaje devuelto
            var response = new HttpResponseMessage();

            //Creo una solicitud Http de tipo POST
            var request = new HttpRequestMessage(HttpMethod.Post, $"api/CarrosComprasMov/CrearMovimiento");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);
            //Agrego el JSON al BODY
            request.Content = movJson;
            //Envio la solicitud HTTP
            response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<IEnumerable<HistorialMovimientoCarro>> ObtenerHistorial(int idOrden)
        {
            var response = await _httpClient.GetStreamAsync($"api/CarrosComprasMov/ObtenerHistorial/{idOrden}");

            var historial = await JsonSerializer.DeserializeAsync<IEnumerable<HistorialMovimientoCarro>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return historial;
        }
    }
}
