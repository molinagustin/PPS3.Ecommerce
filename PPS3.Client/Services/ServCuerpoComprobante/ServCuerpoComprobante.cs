namespace PPS3.Client.Services.ServCuerpoComprobante
{
    public class ServCuerpoComprobante : IServCuerpoComprobante
    {
        private readonly HttpClient _httpClient;

        public ServCuerpoComprobante(HttpClient httpClient) => _httpClient = httpClient;
        
        public async Task<bool> CrearCuerpo(CuerpoComprobante cuerpoComp)
        {
            var cuerpoJson = new StringContent(JsonSerializer.Serialize(cuerpoComp), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"api/CuerposComprobantes/CrearCuerpoComp", cuerpoJson);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<CuerpoComprobante> ObtenerCuerpo(int numCuerpo)
        {
            var response = await _httpClient.GetStreamAsync($"api/CuerposComprobantes/ObtenerCuerpoComp?numCuerpo={numCuerpo}");

            var cuerpo = await JsonSerializer.DeserializeAsync<CuerpoComprobante>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return cuerpo;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<IEnumerable<CuerpoComprobante>> ObtenerCuerpos(int numCabeceraComp)
        {
            var response = await _httpClient.GetStreamAsync($"api/CuerposComprobantes/ObtenerCuerposDeComprobante?numCabeceraComp={numCabeceraComp}");

            var cuerpos = await JsonSerializer.DeserializeAsync<IEnumerable<CuerpoComprobante>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return cuerpos;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }
    }
}
