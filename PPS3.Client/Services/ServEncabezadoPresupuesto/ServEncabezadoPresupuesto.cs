namespace PPS3.Client.Services.ServEncabezadoPresupuesto
{
    public class ServEncabezadoPresupuesto : IServEncabezadoPresupuesto
    {
        private readonly HttpClient _httpClient;

        public ServEncabezadoPresupuesto(HttpClient httpClient) => _httpClient = httpClient;
        
        public async Task<bool> CrearEncabezado(EncabezadoPresupuesto encabPres)
        {
            var encabJson = new StringContent(JsonSerializer.Serialize(encabPres), Encoding.UTF8, "application/json");

            var result = await _httpClient.PostAsync($"api/EncabezadosPresupuestos", encabJson);

            if(result.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<EncabezadoPresupuesto> ObtenerEncabezado(int id)
        {
            var response = await _httpClient.GetStreamAsync($"api/EncabezadosPresupuestos/{id}");

            var encab = await JsonSerializer.DeserializeAsync<EncabezadoPresupuesto>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return encab;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<IEnumerable<EncabezadoPresupuesto>> ObtenerEncabezados()
        {
            var response = await _httpClient.GetStreamAsync($"api/EncabezadosPresupuestos");

            var encabs = await JsonSerializer.DeserializeAsync<IEnumerable<EncabezadoPresupuesto>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return encabs;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }
    }
}
