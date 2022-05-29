using PPS3.Shared.Models;

namespace PPS3.Client.Services.ServLocalidad
{
    public class ServLocalidad : IServLocalidad
    {
        private readonly HttpClient _httpClient;
        private readonly ISessionStorageService _sessionStorage;

        public ServLocalidad(HttpClient httpClient, ISessionStorageService sessionStorage)
        {
            _httpClient = httpClient;
            _sessionStorage = sessionStorage;
        }

        public async Task<bool> BorrarLocalidad(int id)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return false;

            //Creo una solicitud Http de tipo delete
            var request = new HttpRequestMessage(HttpMethod.Delete, $"api/Localidades/{id}");
            //Agrego el token al Encabezado Http
            request.Headers.Add("Authorization", "Bearer " + token);

            //Envio la solicitud y guardo la respuesta
            var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);

            //Verifico que la respuesta sea exitosa
            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<bool> GuardarLocalidad(Localidad localidad)
        {
            //Obtengo el token de sesion del usuario
            var token = await _sessionStorage.GetItemAsync<string>("token");

            //Verifico que exista un token
            if (String.IsNullOrEmpty(token))
                return false;

            //Se procede a Serializar el contenido del producto por parametro
            var locJson = new StringContent(JsonSerializer.Serialize(localidad), Encoding.UTF8, "application/json");

            //Creo el objeto donde se guardara el mensaje devuelto
            var response = new HttpResponseMessage();

            //Si posee un ID es una actualizacion (PUT)
            if (localidad.IdLocalidad > 0)
            {
                //Creo una solicitud Http de tipo PUT
                var request = new HttpRequestMessage(HttpMethod.Put, $"api/Localidades");
                //Agrego el token al Encabezado Http
                request.Headers.Add("Authorization", "Bearer " + token);
                //Agrego el JSON al BODY
                request.Content = locJson;
                //Envio la solicitud HTTP
                response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            }
            else
            {
                //Creo una solicitud Http de tipo POST
                var request = new HttpRequestMessage(HttpMethod.Post, $"api/Localidades");
                //Agrego el token al Encabezado Http
                request.Headers.Add("Authorization", "Bearer " + token);
                //Agrego el JSON al BODY
                request.Content = locJson;
                //Envio la solicitud HTTP
                response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            }

            if (response.IsSuccessStatusCode)
                return true;
            else
                return false;
        }

        public async Task<Localidad> ObtenerLocalidad(int id)
        {
            var response = await _httpClient.GetStreamAsync($"api/Localidades/{id}");

            var localidad = await JsonSerializer.DeserializeAsync<Localidad>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return localidad;
        }

        public async Task<Localidad> ObtenerLocalidad(string nombreLocalidad)
        {
            var response = await _httpClient.GetStreamAsync($"api/Localidades/{nombreLocalidad}");

            var localidad = await JsonSerializer.DeserializeAsync<Localidad>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return localidad;
        }

        public async Task<IEnumerable<Localidad>> ObtenerLocalidades()
        {
            var response = await _httpClient.GetStreamAsync($"api/Localidades");

            var localidades = await JsonSerializer.DeserializeAsync<IEnumerable<Localidad>>(response, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return localidades;
        }
    }
}
