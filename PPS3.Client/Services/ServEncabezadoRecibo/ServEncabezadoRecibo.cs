namespace PPS3.Client.Services.ServEncabezadoRecibo
{
    public class ServEncabezadoRecibo : IServEncabezadoRecibo
    {
        private readonly HttpClient _httpClient;

        public ServEncabezadoRecibo(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<bool> CrearEncabRec(EncabezadoRecibo encabRec)
        {
            var encabJson = new StringContent(JsonSerializer.Serialize(encabRec), Encoding.UTF8, "application/json");

            var result = await _httpClient.PostAsync($"api/EncabezadosRecibos/CrearNuevoRecibo", encabJson);

            if (result.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<EncabezadoRecibo> ObtenerEncab(int idRecibo)
        {
            var response = await _httpClient.GetStreamAsync($"api/EncabezadosRecibos/ObtenerRecibo/{idRecibo}");

            var recibo = await JsonSerializer.DeserializeAsync<EncabezadoRecibo>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return recibo;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<IEnumerable<EncabezadoRecibo>> ObtenerEncabRecCliente(int idCliente)
        {
            var response = await _httpClient.GetStreamAsync($"api/EncabezadosRecibos/ObtenerRecibosCliente/{idCliente}");

            var recibos = await JsonSerializer.DeserializeAsync<IEnumerable<EncabezadoRecibo>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return recibos;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }
    }
}
