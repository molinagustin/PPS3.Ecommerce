namespace PPS3.Client.Services.ServProvincia
{
    public class ServProvincia : IServProvincia
    {
        private readonly HttpClient _httpClient;

        public ServProvincia(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<Provincia> ObtenerProvincia(int id)
        {
            var response = await _httpClient.GetStreamAsync($"api/Provincias/{id}");

            var prov = await JsonSerializer.DeserializeAsync<Provincia>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return prov;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<Provincia> ObtenerProvincia(string nombreProv)
        {
            var response = await _httpClient.GetStreamAsync($"api/Provincias/{nombreProv}");

            var prov = await JsonSerializer.DeserializeAsync<Provincia>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return prov;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<IEnumerable<Provincia>> ObtenerProvincias()
        {
            var response = await _httpClient.GetStreamAsync($"api/Provincias");

            var provincias = await JsonSerializer.DeserializeAsync<IEnumerable<Provincia>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return provincias;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }
    }
}
