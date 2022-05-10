namespace PPS3.Client.Services.ServTipoComprobante
{
    public class ServTipoComprobante : IServTipoComprobante
    {
        private readonly HttpClient _httpClient;

        public ServTipoComprobante(HttpClient httpClient) => _httpClient = httpClient;
        
        public async Task<TipoComprobante> ObtenerTipoComp(int id)
        {
            var response = await _httpClient.GetStreamAsync($"api/TiposComprobantes/{id}");

            var tipoComp = await JsonSerializer.DeserializeAsync<TipoComprobante>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return tipoComp;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<IEnumerable<TipoComprobante>> ObtenerTiposComp()
        {
            var response = await _httpClient.GetStreamAsync($"api/TiposComprobantes");

            var tiposComp = await JsonSerializer.DeserializeAsync<IEnumerable<TipoComprobante>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return tiposComp;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }
    }
}
