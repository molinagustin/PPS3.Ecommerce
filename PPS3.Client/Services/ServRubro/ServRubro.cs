namespace PPS3.Client.Services.ServRubro
{
    public class ServRubro : IServRubro
    {
        private readonly HttpClient _httpClient;

        public ServRubro(HttpClient httpClient) => _httpClient = httpClient;
        
        public async Task<bool> BorrarRubro(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/Rubros/{id}");

            if (result.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<bool> GuardarRubro(Rubro rubro)
        {
            var rubJson = new StringContent(JsonSerializer.Serialize(rubro), Encoding.UTF8, "application/json");

            HttpResponseMessage result = new HttpResponseMessage();

            if (rubro.IdRubro > 0)
                result = await _httpClient.PutAsync($"api/Rubros", rubJson);
            else
                result = await _httpClient.PostAsync($"api/Rubros", rubJson);

            if (result.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<Rubro> ObtenerRubro(int id)
        {
            var response = await _httpClient.GetStreamAsync($"api/Rubros/{id}");

            var rubro = await JsonSerializer.DeserializeAsync<Rubro>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return rubro;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<IEnumerable<Rubro>> ObtenerRubros()
        {
            var response = await _httpClient.GetStreamAsync($"api/Rubros");

            var rubros = await JsonSerializer.DeserializeAsync<IEnumerable<Rubro>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return rubros;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }
    }
}
