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

            return prov;
        }

        public async Task<Provincia> ObtenerProvincia(string nombreProv)
        {
            var response = await _httpClient.GetStreamAsync($"api/Provincias/{nombreProv}");

            var prov = await JsonSerializer.DeserializeAsync<Provincia>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return prov;
        }

        public async Task<IEnumerable<Provincia>> ObtenerProvincias()
        {
            var response = await _httpClient.GetStreamAsync($"api/Provincias");

            var provincias = await JsonSerializer.DeserializeAsync<IEnumerable<Provincia>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return provincias;
        }
    }
}
