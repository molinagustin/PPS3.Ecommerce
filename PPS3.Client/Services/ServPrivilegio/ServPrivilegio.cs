namespace PPS3.Client.Services.ServPrivilegio
{
    public class ServPrivilegio : IServPrivilegio
    {
        private readonly HttpClient _httpClient;

        public ServPrivilegio(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<Privilegio> ObtenerPrivilegio(int id)
        {
            var response = await _httpClient.GetStreamAsync($"api/Privilegios/{id}");

            var privilegio = await JsonSerializer.DeserializeAsync<Privilegio>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return privilegio;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<IEnumerable<Privilegio>> ObtenerPrivilegios()
        {
            var response = await _httpClient.GetStreamAsync($"api/Privilegios");

            var privilegios = await JsonSerializer.DeserializeAsync<IEnumerable<Privilegio>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return privilegios;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }
    }
}
