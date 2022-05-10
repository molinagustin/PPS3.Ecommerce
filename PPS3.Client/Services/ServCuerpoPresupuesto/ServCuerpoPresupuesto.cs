namespace PPS3.Client.Services.ServCuerpoPresupuesto
{
    public class ServCuerpoPresupuesto : IServCuerpoPresupuesto
    {
        private readonly HttpClient _httpClient;

        public ServCuerpoPresupuesto(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<bool> CrearCuerpo(CuerpoPresupuesto cuerpoPresupuesto)
        {
            var cuerpoJson = new StringContent(JsonSerializer.Serialize(cuerpoPresupuesto), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync($"api/CuerposPresupuestos/CrearCuerpoPresupuesto", cuerpoJson);

            if(response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<CuerpoPresupuesto> ObtenerCuerpo(int idCuerpo)
        {
            var response = await _httpClient.GetStreamAsync($"api/CuerposPresupuestos/ObtenerCuerpoPresupuesto?idCuerpo={idCuerpo}");

            var cuerpo = await JsonSerializer.DeserializeAsync<CuerpoPresupuesto>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive=true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return cuerpo;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<IEnumerable<CuerpoPresupuesto>> ObtenerCuerpos(int numPresupuesto)
        {
            var response = await _httpClient.GetStreamAsync($"api/CuerposPresupuestos/ObtenerCuerposDePresupuesto?numPresupuesto={numPresupuesto}");

            var cuerpos = await JsonSerializer.DeserializeAsync<IEnumerable<CuerpoPresupuesto>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return cuerpos;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }
    }
}
