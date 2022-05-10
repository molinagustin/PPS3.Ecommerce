namespace PPS3.Client.Services.ServProveedor
{
    public class ServProveedor : IServProveedor
    {
        private readonly HttpClient _httpClient;

        public ServProveedor(HttpClient httpClient) => _httpClient = httpClient;
        
        public async Task<bool> BorrarProveedor(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/Proveedores/{id}");

            //Verifico que se haya producido la comunicacion correctamente
            if (result.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<bool> GuardarProveedor(Proveedor proveedor)
        {
            var provJson = new StringContent(JsonSerializer.Serialize(proveedor), Encoding.UTF8, "application/json");

            HttpResponseMessage result = new HttpResponseMessage();

            if (proveedor.IdProveedor > 0)
                result = await _httpClient.PutAsync($"api/Proveedores", provJson);
            else
                result = await _httpClient.PostAsync($"api/Proveedores", provJson);

            if (result.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<Proveedor> ObtenerProveedor(int id)
        {
            var response = await _httpClient.GetStreamAsync($"api/Proveedores/{id}");

            var proveedor = await JsonSerializer.DeserializeAsync<Proveedor>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return proveedor;
        }

        public async Task<IEnumerable<Proveedor>> ObtenerProveedores()
        {
            var response = await _httpClient.GetStreamAsync($"api/Proveedores");

            var proveedores = await JsonSerializer.DeserializeAsync<IEnumerable<Proveedor>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return proveedores;
        }
    }
}
