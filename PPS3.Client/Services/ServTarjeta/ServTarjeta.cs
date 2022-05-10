namespace PPS3.Client.Services.ServTarjeta
{
    public class ServTarjeta : IServTarjeta
    {
        private readonly HttpClient _httpClient;

        public ServTarjeta(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<Tarjeta> ObtenerTarjeta(int id)
        {
            var response = await _httpClient.GetStreamAsync($"api/Tarjetas/{id}");

            var tarjeta = await JsonSerializer.DeserializeAsync<Tarjeta>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return tarjeta;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<IEnumerable<Tarjeta>> ObtenerTarjetas()
        {
            var response = await _httpClient.GetStreamAsync($"api/Tarjetas");

            var tarjetas = await JsonSerializer.DeserializeAsync<IEnumerable<Tarjeta>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return tarjetas;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }
    }
}
