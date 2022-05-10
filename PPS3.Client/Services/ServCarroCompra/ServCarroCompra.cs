namespace PPS3.Client.Services.ServCarroCompra
{
    public class ServCarroCompra : IServCarroCompra
    {
        private readonly HttpClient _httpClient;

        public ServCarroCompra(HttpClient httpClient) => _httpClient = httpClient;
        
        public async Task<bool> GuardarCarro(CarroCompra carro)
        {
            var carroJson = new StringContent(JsonSerializer.Serialize(carro), Encoding.UTF8, "application/json");

            var response = new HttpResponseMessage();

            if (carro.IdCarro > 0)
                response = await _httpClient.PutAsync($"api/CarrosCompras", carroJson);
            else
                response = await _httpClient.PostAsync($"api/CarrosCompras", carroJson);

            if(response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<CarroCompra> ObtenerCarro(int id)
        {
            var response = await _httpClient.GetStreamAsync($"api/CarrosCompras/{id}");

            var carro = await JsonSerializer.DeserializeAsync<CarroCompra>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return carro;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<IEnumerable<CarroCompra>> ObtenerCarros()
        {
            var response = await _httpClient.GetStreamAsync($"api/CarrosCompras");

            var carros = await JsonSerializer.DeserializeAsync<IEnumerable<CarroCompra>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return carros;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }
    }
}
