namespace PPS3.Client.Services.ServUnidadMedida
{
    public class ServUnidadMedida : IServUnidadMedida
    {
        private readonly HttpClient _httpClient;

        public ServUnidadMedida(HttpClient httpClient) => _httpClient = httpClient;
        
        public async Task<bool> BorrarUnidad(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/UnidadesMedida/{id}");

            if (result.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<bool> GuardarUnidad(UnidadMedida unidad)
        {
            var uniJson = new StringContent(JsonSerializer.Serialize(unidad), Encoding.UTF8, "application/json");

            HttpResponseMessage result = new HttpResponseMessage();

            if (unidad.IdUnidad > 0)
                result = await _httpClient.PutAsync($"api/UnidadesMedida", uniJson);
            else
                result = await _httpClient.PostAsync($"api/UnidadesMedida", uniJson);

            if (result.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<UnidadMedida> ObtenerUnidad(int id)
        {
            var response = await _httpClient.GetStreamAsync($"api/UnidadesMedida/{id}");

            var unidad = await JsonSerializer.DeserializeAsync<UnidadMedida>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return unidad;
        }

        public async Task<IEnumerable<UnidadMedida>> ObtenerUnidades()
        {
            var response = await _httpClient.GetStreamAsync($"api/UnidadesMedida");

            var unidades = await JsonSerializer.DeserializeAsync<IEnumerable<UnidadMedida>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return unidades;
        }
    }
}
