namespace PPS3.Client.Services.ServTipoProducto
{
    public class ServTipoProducto : IServTipoProducto
    {
        private readonly HttpClient _httpClient;

        public ServTipoProducto(HttpClient httpClient) => _httpClient = httpClient;
        
        public async Task<bool> BorrarTipoProd(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/TiposProductos/{id}");

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<bool> GuardarTipoProd(TipoProducto tipoProd)
        {
            var tipoProdJson = new StringContent(JsonSerializer.Serialize(tipoProd), Encoding.UTF8, "application/json");

            HttpResponseMessage result = new HttpResponseMessage();

            if (tipoProd.IdTipo > 0)
                result = await _httpClient.PutAsync("api/TiposProductos", tipoProdJson);
            else
                result = await _httpClient.PostAsync("api/TiposProductos", tipoProdJson);

            if (result.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<TipoProducto> ObtenerTipoProd(int id)
        {
            var response = await _httpClient.GetStreamAsync($"api/TiposProductos/{id}");

            var tipoProd = await JsonSerializer.DeserializeAsync<TipoProducto>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return tipoProd;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<IEnumerable<TipoProducto>> ObtenerTiposProd()
        {
            var response = await _httpClient.GetStreamAsync("api/TiposProductos");

            var tiposProd = await JsonSerializer.DeserializeAsync<IEnumerable<TipoProducto>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return tiposProd;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }
    }
}
