namespace PPS3.Client.Services.ServTipoTarjeta
{
    public class ServTipoTarjeta : IServTipoTarjeta
    {
        private readonly HttpClient _httpClient;

        public ServTipoTarjeta(HttpClient httpClient) => _httpClient = httpClient;
        
        public async Task<IEnumerable<TipoTarjeta>> ObtenerTiposTarj()
        {
            var response = await _httpClient.GetStreamAsync("api/TiposTarjetas");

            var tiposTarj = await JsonSerializer.DeserializeAsync<IEnumerable<TipoTarjeta>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return tiposTarj;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<TipoTarjeta> ObtenerTipoTarj(int id)
        {
            var response = await _httpClient.GetStreamAsync($"api/TiposTarjetas/{id}");

            var tipoTarj = await JsonSerializer.DeserializeAsync<TipoTarjeta>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return tipoTarj;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }
    }
}
