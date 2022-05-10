namespace PPS3.Client.Services.ServDetalleCarroCompra
{
    public class ServDetalleCarroCompra : IServDetalleCarroCompra
    {
        private readonly HttpClient _httpClient;

        public ServDetalleCarroCompra(HttpClient httpClient) => _httpClient = httpClient;
        
        public async Task<bool> BorrarDetalle(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/DetallesCarrosCompras/BorrarDetalleCarro/{id}");

            if(response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<bool> GuardarDetalle(DetalleCarroCompra detalle)
        {
            var detalleJson = new StringContent(JsonSerializer.Serialize(detalle), Encoding.UTF8, "application/json");

            HttpResponseMessage response = new HttpResponseMessage();

            if (detalle.IdDetalle > 0)
                response = await _httpClient.PutAsync($"api/DetallesCarrosCompras/ActualizarDetalleCarro", detalleJson);
            else
                response = await _httpClient.PostAsync($"api/DetallesCarrosCompras/CrearDetalleCarro", detalleJson);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<DetalleCarroCompra> ObtenerDetalle(int id)
        {
            var response = await _httpClient.GetStreamAsync($"api/DetallesCarrosCompras/ObtenerDetalle/{id}");

            var detalle = await JsonSerializer.DeserializeAsync<DetalleCarroCompra>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return detalle;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<IEnumerable<DetalleCarroCompra>> ObtenerDetallesCarro(int idCarro)
        {
            var response = await _httpClient.GetStreamAsync($"api/DetallesCarrosCompras/ObtenerDetallesCarro?idCarro={idCarro}");

            var detalles = await JsonSerializer.DeserializeAsync<IEnumerable<DetalleCarroCompra>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return detalles;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<IEnumerable<DetalleCarroCompra>> ObtenerTodosDetalles()
        {
            var response = await _httpClient.GetStreamAsync($"api/DetallesCarrosCompras/ObtenerTodosDetalles");

            var detalles = await JsonSerializer.DeserializeAsync<IEnumerable<DetalleCarroCompra>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return detalles;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }
    }
}
