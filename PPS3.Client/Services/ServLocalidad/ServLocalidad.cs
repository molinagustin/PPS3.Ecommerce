namespace PPS3.Client.Services.ServLocalidad
{
    public class ServLocalidad : IServLocalidad
    {
        private readonly HttpClient _httpClient;

        public ServLocalidad(HttpClient httpClient) => _httpClient = httpClient;

        public async Task<bool> BorrarLocalidad(int id)
        {
            var result = await _httpClient.DeleteAsync($"api/Localidades/{id}");

            if (result.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<bool> GuardarLocalidad(Localidad localidad)
        {
            var localJson = new StringContent(JsonSerializer.Serialize(localidad), Encoding.UTF8, "application/json");

            HttpResponseMessage response = new HttpResponseMessage();

            if (localidad.IdLocalidad > 0)
                response = await _httpClient.PutAsync($"api/Localidades", localJson);
            else
                response = await _httpClient.PostAsync($"api/Localidades", localJson);

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<Localidad> ObtenerLocalidad(int id)
        {
            var response = await _httpClient.GetStreamAsync($"api/Localidades/{id}");

            var localidad = await JsonSerializer.DeserializeAsync<Localidad>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return localidad;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<Localidad> ObtenerLocalidad(string nombreLocalidad)
        {
            var response = await _httpClient.GetStreamAsync($"api/Localidades/{nombreLocalidad}");

            var localidad = await JsonSerializer.DeserializeAsync<Localidad>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return localidad;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }

        public async Task<IEnumerable<Localidad>> ObtenerLocalidades()
        {
            var response = await _httpClient.GetStreamAsync($"api/Localidades");

            var localidades = await JsonSerializer.DeserializeAsync<IEnumerable<Localidad>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

#pragma warning disable CS8603 // Posible tipo de valor devuelto de referencia nulo
            return localidades;
#pragma warning restore CS8603 // Posible tipo de valor devuelto de referencia nulo
        }
    }
}
