namespace PPS3.Client.Services.ServEncabezadoCuentaCorriente
{
    public class ServEncabezadoCuentaCorriente : IServEncabezadoCuentaCorriente
    {
        private readonly HttpClient _httpClient;

        public ServEncabezadoCuentaCorriente(HttpClient httpClient) => _httpClient = httpClient;
        
        public async Task<bool> BorrarEncabCC(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/EncabezadosCuentasCorrientes/BorrarCuentaCorriente/{id}");

            if(result.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<bool> GuardarEncabCC(EncabezadoCuentaCorriente encabezadoCC)
        {
            var encabJson = new StringContent(JsonSerializer.Serialize(encabezadoCC), Encoding.UTF8, "application/json");

            HttpResponseMessage response = new HttpResponseMessage();

            if (encabezadoCC.NumCC > 0)
                response = await _httpClient.PutAsync($"api/EncabezadosCuentasCorrientes/ActualizarCuentaCorriente", encabJson);
            else
                response = await _httpClient.PostAsync($"api/EncabezadosCuentasCorrientes/CrearCuentaCorriente", encabJson);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<EncabezadoCuentaCorriente> ObtenerCCCliente(int idCliente)
        {
            var response = await _httpClient.GetStreamAsync($"api/EncabezadosCuentasCorrientes/ObtenerCCCliente?idCliente={idCliente}");

            var cc = await JsonSerializer.DeserializeAsync<EncabezadoCuentaCorriente>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return cc;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<EncabezadoCuentaCorriente> ObtenerCuentaCorriente(int numCC)
        {
            var response = await _httpClient.GetStreamAsync($"api/EncabezadosCuentasCorrientes/ObtenerCuentaCorriente?numCC={numCC}");

            var cc = await JsonSerializer.DeserializeAsync<EncabezadoCuentaCorriente>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return cc;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<IEnumerable<EncabezadoCuentaCorriente>> ObtenerCuentasCorrientes()
        {
            var response = await _httpClient.GetStreamAsync($"api/EncabezadosCuentasCorrientes/ObtenerCuentasCorrientes");

            var cuentas = await JsonSerializer.DeserializeAsync<IEnumerable<EncabezadoCuentaCorriente>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return cuentas;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }
    }
}
