namespace PPS3.Client.Services.ServEncabezadoComprobante
{
    public class ServEncabezadoComprobante : IServEncabezadoComprobante
    {
        private readonly HttpClient _httpClient;

        public ServEncabezadoComprobante(HttpClient httpClient) => _httpClient = httpClient;
        
        public async Task<int> CrearEncabezado(EncabezadoComprobante encabezadoComp)
        {
            var encabJson = new StringContent(JsonSerializer.Serialize(encabezadoComp), Encoding.UTF8, "application/json");

            var encab = await _httpClient.PostAsync($"api/EncabezadosComprobantes", encabJson);

            //PROBAR SI CON ESE METODO DEVUELVE EL NUMERO DE ENCABEZADO CREADO
            if (encab.IsSuccessStatusCode)
                return await encab.Content.ReadFromJsonAsync<int>();
            else
                return 0;                
        }

        public async Task<EncabezadoComprobante> ObtenerEncabezado(int id)
        {
            var response = await _httpClient.GetStreamAsync($"api/EncabezadosComprobantes/{id}");

            var encabezado = await JsonSerializer.DeserializeAsync<EncabezadoComprobante>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return encabezado;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<IEnumerable<EncabezadoComprobante>> ObtenerEncabezados()
        {
            var response = await _httpClient.GetStreamAsync($"api/EncabezadosComprobantes");

            var encabezados = await JsonSerializer.DeserializeAsync<IEnumerable<EncabezadoComprobante>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return encabezados;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }
    }
}
