namespace PPS3.Client.Services.ServReporte
{
    public class ServReporte : IServReporte
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorage;

        public ServReporte(HttpClient httpClient, ISessionStorageService sessionStorage)
        {
            _httpClient = httpClient;
            _sessionStorage = sessionStorage;
        }

        public async Task<IEnumerable<Ganancia>> ReporteGanancias(Parametros parametros)
        {
            var token = await _sessionStorage.GetItemAsync<string>("token");
            if (String.IsNullOrEmpty(token)) return null;

            var request = new HttpRequestMessage(HttpMethod.Post, $"api/Reportes/ReporteGanancias");
            request.Headers.Add("Authorization", "Bearer " + token);

            var parametrosJson = new StringContent(JsonSerializer.Serialize(parametros), Encoding.UTF8, "application/json");
            request.Content = parametrosJson;

            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var ganancias = await JsonSerializer.DeserializeAsync<IEnumerable<Ganancia>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return ganancias;
            }
            else return null;
        }

        public async Task<IEnumerable<MasProducto>> ReporteMasProductos(Parametros parametros)
        {
            var token = await _sessionStorage.GetItemAsync<string>("token");
            if (String.IsNullOrEmpty(token)) return null;

            var request = new HttpRequestMessage(HttpMethod.Post, $"api/Reportes/ReporteMasProductos");
            request.Headers.Add("Authorization", "Bearer " + token);

            var parametrosJson = new StringContent(JsonSerializer.Serialize(parametros), Encoding.UTF8, "application/json");
            request.Content = parametrosJson;

            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var productos = await JsonSerializer.DeserializeAsync<IEnumerable<MasProducto>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return productos;
            }
            else return null;
        }

        public async Task<IEnumerable<ProductoFecha>> ReporteProductosFecha(Parametros parametros)
        {
            var token = await _sessionStorage.GetItemAsync<string>("token");
            if (String.IsNullOrEmpty(token)) return null;

            var request = new HttpRequestMessage(HttpMethod.Post, $"api/Reportes/ReporteProductosFecha");
            request.Headers.Add("Authorization", "Bearer " + token);

            var parametrosJson = new StringContent(JsonSerializer.Serialize(parametros), Encoding.UTF8, "application/json");
            request.Content = parametrosJson;

            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var productos = await JsonSerializer.DeserializeAsync<IEnumerable<ProductoFecha>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return productos;
            }
            else return null;
        }

        public async Task<IEnumerable<StockProd>> ReporteStockProductos(Parametros parametros)
        {
            var token = await _sessionStorage.GetItemAsync<string>("token");
            if (String.IsNullOrEmpty(token)) return null;

            var request = new HttpRequestMessage(HttpMethod.Post, $"api/Reportes/ReporteStockProductos");
            request.Headers.Add("Authorization", "Bearer " + token);

            var parametrosJson = new StringContent(JsonSerializer.Serialize(parametros), Encoding.UTF8, "application/json");
            request.Content = parametrosJson;

            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var stockProds = await JsonSerializer.DeserializeAsync<IEnumerable<StockProd>>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return stockProds;
            }
            else return null;
        }
    }
}
