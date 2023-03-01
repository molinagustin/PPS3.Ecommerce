using PPS3.Shared.Models;

namespace PPS3.Client.Services.ServHerramientas
{
    public class ServHerramientas : IServHerramientas
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorage;

        public ServHerramientas(HttpClient httpClient, ISessionStorageService sessionStorage)
        {
            _httpClient = httpClient;
            _sessionStorage = sessionStorage;
        }

        public async Task<int> CambiarPrecios(CambioPrecios cambios)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token)) return 0;

            var cambiosJson = new StringContent(JsonSerializer.Serialize(cambios), Encoding.UTF8, "application/json");

            //Creo una solicitud Http
            var request = new HttpRequestMessage(HttpMethod.Post, $"api/Herramientas/CambiarPrecios");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);
            //Agrego el objeto json
            request.Content = cambiosJson;

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Si la respuesta es exitosa, leo el contenido como STREAM (flujo de bits) y lo deserializo en un objeto apropiado
            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                var cantidad = await JsonSerializer.DeserializeAsync<int>(stream, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

                return cantidad;
            }
            else return 0;
        }
    }
}