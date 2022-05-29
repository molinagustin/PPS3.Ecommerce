namespace PPS3.Client.Services.ServTipoDocumento
{
    public class ServTipoDocumento : IServTipoDocumento
    {
        private readonly HttpClient _httpClient;

        public ServTipoDocumento(HttpClient httpClient) => _httpClient = httpClient;
        
        public async Task<TipoDocumento> ObtenerTipoDoc(int id)
        {
            var response = await _httpClient.GetStreamAsync($"api/TiposDocumentos/{id}");

            var tipoDoc = await JsonSerializer.DeserializeAsync<TipoDocumento>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return tipoDoc;
        }

        public async Task<IEnumerable<TipoDocumento>> ObtenerTiposDocs()
        {
            var response = await _httpClient.GetStreamAsync($"api/TiposDocumentos");

            var tiposDocs = await JsonSerializer.DeserializeAsync<IEnumerable<TipoDocumento>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return tiposDocs;
        }
    }
}
