namespace PPS3.Client.Services.ServEstadoCarroCompra
{
    public class ServEstadoCarroCompra : IServEstadoCarroCompra
    {
        private readonly HttpClient _httpClient;

        public ServEstadoCarroCompra(HttpClient httpClient) => _httpClient = httpClient;
        
        public async Task<EstadoCarroCompra> ObtenerEstado(int id)
        {
            var response = await _httpClient.GetStreamAsync($"api/EstadosCarrosCompras/{id}");

            var estado = await JsonSerializer.DeserializeAsync<EstadoCarroCompra>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return estado;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<IEnumerable<EstadoCarroCompra>> ObtenerEstados()
        {
            var response = await _httpClient.GetStreamAsync($"api/EstadosCarrosCompras");

            var estados = await JsonSerializer.DeserializeAsync<IEnumerable<EstadoCarroCompra>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return estados;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }
    }
}
