namespace PPS3.Client.Services.ServFormaPago
{
    public class ServFormaPago : IServFormaPago
    {
        private readonly HttpClient _httpClient;

        public ServFormaPago(HttpClient httpClient) => _httpClient = httpClient;
        
        public async Task<FormaPago> ObtenerFormaPago(int id)
        {
            var response = await _httpClient.GetStreamAsync($"api/FormasPago/{id}");

            var forma = await JsonSerializer.DeserializeAsync<FormaPago>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return forma;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<IEnumerable<FormaPago>> ObtenerFormasPago()
        {
            var response = await _httpClient.GetStreamAsync($"api/FormasPago");

            var formas = await JsonSerializer.DeserializeAsync<IEnumerable<FormaPago>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return formas;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }
    }
}
