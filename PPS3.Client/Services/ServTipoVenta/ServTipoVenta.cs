namespace PPS3.Client.Services.ServTipoVenta
{
    public class ServTipoVenta : IServTipoVenta
    {
        private readonly HttpClient _httpClient;

        public ServTipoVenta(HttpClient httpClient) => _httpClient = httpClient;
        
        public async Task<IEnumerable<TipoVenta>> ObtenerTiposVentas()
        {
            var response = await _httpClient.GetStreamAsync("api/TiposVentas");

            var tiposVta = await JsonSerializer.DeserializeAsync<IEnumerable<TipoVenta>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return tiposVta;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<TipoVenta> ObtenerTipoVenta(int id)
        {
            var response = await _httpClient.GetStreamAsync($"api/TiposVentas/{id}");

            var tipoVta = await JsonSerializer.DeserializeAsync<TipoVenta>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return tipoVta;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }
    }
}
