namespace PPS3.Client.Services.ServDetalleRecibo
{
    public class ServDetalleRecibo : IServDetalleRecibo
    {
        private readonly HttpClient _httpClient;

        public ServDetalleRecibo(HttpClient httpClient) => _httpClient = httpClient;
        
        public async Task<bool> CrearDetalleRecibo(DetalleRecibo detalleRec)
        {
            var detalleJson = new StringContent(JsonSerializer.Serialize(detalleRec), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"api/DetallesRecibos/CrearDetalleRecibo",detalleJson);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<IEnumerable<DetalleRecibo>> ObtenerDetallesPorComprobante(int idComprobante)
        {
            var response = await _httpClient.GetStreamAsync($"api/DetallesRecibos/ObtenerDetallesPorComprobante?idComprobante={idComprobante}");

            var detalles = await JsonSerializer.DeserializeAsync<IEnumerable<DetalleRecibo>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return detalles;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<IEnumerable<DetalleRecibo>> ObtenerDetallesRecibo(int idRecibo)
        {
            var response = await _httpClient.GetStreamAsync($"api/DetallesRecibos/ObtenerDetallesRecibo?idRecibo={idRecibo}");

            var detalles = await JsonSerializer.DeserializeAsync<IEnumerable<DetalleRecibo>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return detalles;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }
    }
}
