namespace PPS3.Client.Services.ServCliente
{
    public class ServCliente : IServCliente
    {
        private readonly HttpClient _httpClient;

        public ServCliente(HttpClient httpClient) => _httpClient = httpClient;
        public async Task<Cliente> ObtenerCliente(int id)
        {
            var response = await _httpClient.GetStreamAsync($"api/Clientes/{id}");

            var cliente = await JsonSerializer.DeserializeAsync<Cliente>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return cliente;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<Cliente> ObtenerCliente(string nombreCliente)
        {
            var response = await _httpClient.GetStreamAsync($"api/Clientes/{nombreCliente}");

            var cliente = await JsonSerializer.DeserializeAsync<Cliente>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return cliente;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<IEnumerable<Cliente>> ObtenerClientes()
        {
            var response = await _httpClient.GetStreamAsync($"api/Clientes");

            var clientes = await JsonSerializer.DeserializeAsync<IEnumerable<Cliente>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return clientes;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }
    }
}
