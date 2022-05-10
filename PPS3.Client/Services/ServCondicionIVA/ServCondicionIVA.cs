namespace PPS3.Client.Services.ServCondicionIVA
{
    public class ServCondicionIVA : IServCondicionIVA
    {
        private readonly HttpClient _httpClient;

        public ServCondicionIVA(HttpClient httpClient) => _httpClient = httpClient;
       
        public async Task<CondicionIVA> ObtenerCondicion(int id)
        {
            var response = await _httpClient.GetStreamAsync($"api/CondicionesIVA/{id}");

            var condicion = await JsonSerializer.DeserializeAsync<CondicionIVA>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return condicion;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<IEnumerable<CondicionIVA>> ObtenerCondiciones()
        {
            var response = await _httpClient.GetStreamAsync($"api/CondicionesIVA");

            var condiciones = await JsonSerializer.DeserializeAsync<IEnumerable<CondicionIVA>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return condiciones;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }
    }
}
